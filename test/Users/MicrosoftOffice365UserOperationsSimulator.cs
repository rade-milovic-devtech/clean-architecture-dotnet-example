using Moq;
using Office365.UserManagement.Core.Customers;
using Office365.UserManagement.Core.Users;

namespace Office365.UserManagement.Users
{
	internal class MicrosoftOffice365UserOperationsSimulator
		: IOperateOnMicrosoftOffice365Users
	{
		private readonly Mock<IOperateOnMicrosoftOffice365Users> microsoftOffice365UserOperationsMock =
			new Mock<IOperateOnMicrosoftOffice365Users>();

		public void DeleteUserWith(CustomerCspId customerCspId, UserName userName)
		{
			microsoftOffice365UserOperationsMock.Object.DeleteUserWith(customerCspId, userName);
		}

		public void HasDeletedAUserWith(string cspCustomerId, string userName)
		{
			microsoftOffice365UserOperationsMock.Verify(microsoftOffice365UserOperations =>
				microsoftOffice365UserOperations.DeleteUserWith(
					new CustomerCspId(cspCustomerId), new UserName(userName)),
				Times.Once);
		}
	}
}