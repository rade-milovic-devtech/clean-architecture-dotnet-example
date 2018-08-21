using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Office365.UserManagement.WebApi.Users
{
	[Trait("Category", "Integration")]
	public class UsersControllerTests : IClassFixture<WebApiTestsFixture>
	{
		private const string ACustomerNumber = "1234";
		private const string AUserName = "tester@testdomain.onmicrosoft.com";

		private readonly HttpClient httpClient;
		private readonly UserOperationsSimulator userOperations;

		public UsersControllerTests(WebApiTestsFixture fixture)
		{
			httpClient = fixture.HttpClient;
			userOperations = fixture.UserOperations;
		}

		[Fact]
		public async Task SuccessfullyDeletesCustomerUser()
		{
			await httpClient.DeleteAsync(DeleteCustomerUserUrlFor(ACustomerNumber, AUserName));

			userOperations.HasDeletedAUserWith(ACustomerNumber, AUserName);
		}

		[Fact]
		public async Task ReturnsNoContentStatusCodeWhenCustomerUserIsDeletedSuccessfully()
		{
			var response = await httpClient.DeleteAsync(DeleteCustomerUserUrlFor(ACustomerNumber, AUserName));

			response.StatusCode.Should().Be(HttpStatusCode.NoContent);
		}

		private string DeleteCustomerUserUrlFor(string customerNumber, string userName) =>
			$"api/customers/{customerNumber}/users/{userName}";
	}
}