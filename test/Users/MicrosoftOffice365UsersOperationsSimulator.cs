using Moq;
using Office365.UserManagement.Core.Customers;
using Office365.UserManagement.Core.Subscriptions;
using Office365.UserManagement.Core.Users;
using System.Collections.Generic;
using System.Linq;

namespace Office365.UserManagement.Users
{
	internal class MicrosoftOffice365UsersOperationsSimulator
		: IOperateOnMicrosoftOffice365Users
	{
		private string customerId = string.Empty;
		private string userName = string.Empty;

		private readonly Mock<IOperateOnMicrosoftOffice365Users> microsoftOffice365UserOperationsMock =
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

		public MicrosoftOffice365UsersOperationsSimulator ReturnsSubscriptionIds(params string[] subscriptionIds)
		{
			microsoftOffice365UserOperationsMock.Setup(microsoftOffice365UserOperations =>
				microsoftOffice365UserOperations.GetAssignedSubscriptionIds(
					new CustomerCspId(customerId), new UserName(userName)))
						.Returns(subscriptionIds.Select(id => new SubscriptionCspId(id)));

			return this;
		}

		public IEnumerable<SubscriptionCspId> GetAssignedSubscriptionIds(CustomerCspId customerId, UserName userName) =>
			microsoftOffice365UserOperationsMock.Object.GetAssignedSubscriptionIds(customerId, userName);

		public void DeleteUser(CustomerCspId customerId, UserName userName)
		{
			microsoftOffice365UserOperationsMock.Object.DeleteUser(customerId, userName);
		}

		public void HasDeletedAUserWith(string customerId, string userName)
		{
			microsoftOffice365UserOperationsMock.Verify(microsoftOffice365UserOperations =>
				microsoftOffice365UserOperations.DeleteUser(
					new CustomerCspId(customerId), new UserName(userName)),
				Times.Once);
		}
	}
}