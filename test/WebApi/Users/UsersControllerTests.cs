using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Office365.UserManagement.WebApi.Users
{
	[Trait("Category", "Integration")]
	public class UsersControllerTests : IClassFixture<WebApiTestsFixture>, IDisposable
	{
		private const string ACustomerNumber = "1234";
		private const string AUserName = "tester@testdomain.onmicrosoft.com";
		private const string AUserEmail = "tester@testdomain.onmicrosoft.com";
		private const string AUserFullName = "FirstName LastName";

		private readonly HttpClient httpClient;
		private readonly UserOperationsSimulator userOperations;

		public UsersControllerTests(WebApiTestsFixture fixture)
		{
			httpClient = fixture.HttpClient;
			userOperations = fixture.UserOperations;
		}

		[Fact]
		public async Task ReturnsNotFoundStatusCodeWhenCustomerUserIsNotFound()
		{
			userOperations
				.ForCustomerWithNumber(ACustomerNumber)
				.AndUser(AUserName)
				.PopulatesPresenterDataWithNotFoundResult();

			var response = await httpClient.GetAsync(CustomerUserUrlFor(ACustomerNumber, AUserName));

			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}

		[Fact]
		public async Task ReturnsOkStatusCodeWithUserDetailsWhenCustomerUserIsFound()
		{
			userOperations
				.ForCustomerWithNumber(ACustomerNumber)
				.AndUser(AUserName)
				.PopulatesPresenterDataWithOkResultWith(
					email: AUserEmail,
					fullName: AUserFullName);

			var response = await httpClient.GetAsync(CustomerUserUrlFor(ACustomerNumber, AUserName));

			response.StatusCode.Should().Be(HttpStatusCode.OK);
			var responseContent = await response.Content.ReadAsStringAsync();
			responseContent.Should()
				.Be(UserDetailsResponseJsonWith(AUserEmail, AUserFullName));
		}

		[Fact]
		public async Task SuccessfullyDeletesCustomerUser()
		{
			await httpClient.DeleteAsync(CustomerUserUrlFor(ACustomerNumber, AUserName));

			userOperations.HasDeletedAUserWith(ACustomerNumber, AUserName);
		}

		[Fact]
		public async Task ReturnsNoContentStatusCodeWhenCustomerUserIsDeletedSuccessfully()
		{
			var response = await httpClient.DeleteAsync(CustomerUserUrlFor(ACustomerNumber, AUserName));

			response.StatusCode.Should().Be(HttpStatusCode.NoContent);
		}

		public void Dispose()
		{
			userOperations.ClearAllInvocations();
		}

		private string CustomerUserUrlFor(string customerNumber, string userName) =>
			$"api/customers/{customerNumber}/users/{userName}";

		private string UserDetailsResponseJsonWith(string email, string fullName) =>
			$"{{\"email\":\"{email}\",\"fullName\":\"{fullName}\"}}";
	}
}