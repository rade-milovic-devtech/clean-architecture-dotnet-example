using Moq;
using Office365.UserManagement.Core.Customers;
using Office365.UserManagement.Core.Subscriptions;
using System.Collections.Generic;
using System.Linq;

namespace Office365.UserManagement.Core.Users
{
	public class MicrosoftOffice365UsersOperationsSimulator
		: IOperateOnMicrosoftOffice365Users
	{
		private string customerId = string.Empty;
		private string userName = string.Empty;

		private readonly Mock<IOperateOnMicrosoftOffice365Users> microsoftOffice365UsersOperationsMock =
			new Mock<IOperateOnMicrosoftOffice365Users>();

		public MicrosoftOffice365UsersOperationsSimulator ForCustomerWithId(string customerId)
		{
			this.customerId = customerId;

			return this;
		}

		public MicrosoftOffice365UsersOperationsSimulator AndUser(string userName)
		{
			this.userName = userName;

			return this;
		}

		public MicrosoftOffice365UsersOperationsSimulator ReturnsUser(User user)
		{
			microsoftOffice365UsersOperationsMock.Setup(microsoftOffice365UsersOperations =>
				microsoftOffice365UsersOperations.GetUserDetails(
					new CustomerCspId(customerId), new UserName(userName)))
						.Returns(user);

			return this;
		}

		public MicrosoftOffice365UsersOperationsSimulator ReturnsSubscriptionIds(params string[] subscriptionIds)
		{
			microsoftOffice365UsersOperationsMock.Setup(microsoftOffice365UsersOperations =>
				microsoftOffice365UsersOperations.GetAssignedSubscriptionIds(
					new CustomerCspId(customerId), new UserName(userName)))
						.Returns(subscriptionIds.Select(id => new SubscriptionCspId(id)));

			return this;
		}

		public User GetUserDetails(CustomerCspId customerId, UserName userName) =>
			microsoftOffice365UsersOperationsMock.Object.GetUserDetails(customerId, userName);

		public IEnumerable<SubscriptionCspId> GetAssignedSubscriptionIds(CustomerCspId customerId, UserName userName) =>
			microsoftOffice365UsersOperationsMock.Object.GetAssignedSubscriptionIds(customerId, userName);

		public void DeleteUser(CustomerCspId customerId, UserName userName)
		{
			microsoftOffice365UsersOperationsMock.Object.DeleteUser(customerId, userName);
		}

		public void HasDeletedAUserWith(string customerId, string userName)
		{
			microsoftOffice365UsersOperationsMock.Verify(microsoftOffice365UsersOperations =>
				microsoftOffice365UsersOperations.DeleteUser(
					new CustomerCspId(customerId), new UserName(userName)),
				Times.Once);
		}
	}
}