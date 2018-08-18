using Office365.UserManagement.Core.Customers;

namespace Office365.UserManagement.Core.Users
{
	public class UserOperations
	{
		private readonly IReadCustomerConfiguration customerConfigurationReader;
		private readonly IStoreCustomerInformation customerInformationStore;
		private readonly IOperateOnMicrosoftOffice365Users microsoftOffice365UsersOperations;

		public UserOperations(
			IReadCustomerConfiguration customerConfigurationReader,
			IStoreCustomerInformation customerInformationStore,
			IOperateOnMicrosoftOffice365Users microsoftOffice365UsersOperations)
		{
			this.customerConfigurationReader = customerConfigurationReader;
			this.customerInformationStore = customerInformationStore;
			this.microsoftOffice365UsersOperations = microsoftOffice365UsersOperations;
		}

		public void DeleteUser(DeleteUserCommand command)
		{
		}
	}
}