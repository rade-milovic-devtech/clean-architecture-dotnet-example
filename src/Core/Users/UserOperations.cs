using Office365.UserManagement.Core.Customers;
using Office365.UserManagement.Core.Subscriptions;
using System.Linq;

namespace Office365.UserManagement.Core.Users
{
	public class UserOperations
	{
		private readonly IReadCustomerConfiguration customerConfigurationReader;
		private readonly IStoreCustomerInformation customerInformationStore;
		private readonly IOperateOnMicrosoftOffice365Users microsoftOffice365UsersOperations;
		private readonly IOperateOnMicrosoftOffice365Subscriptions microsoftOffice365SubscriptionsOperations;

		public UserOperations(
			IReadCustomerConfiguration customerConfigurationReader,
			IStoreCustomerInformation customerInformationStore,
			IOperateOnMicrosoftOffice365Users microsoftOffice365UsersOperations,
			IOperateOnMicrosoftOffice365Subscriptions microsoftOffice365SubscriptionsOperations)
		{
			this.customerConfigurationReader = customerConfigurationReader;
			this.customerInformationStore = customerInformationStore;
			this.microsoftOffice365UsersOperations = microsoftOffice365UsersOperations;
			this.microsoftOffice365SubscriptionsOperations = microsoftOffice365SubscriptionsOperations;
		}

		public void DeleteUser(DeleteUserCommand command)
		{
			var customer = customerInformationStore.Get(
				new CustomerNumber(command.CustomerNumber));

			var userName = new UserName(command.UserName);
			var userSubscriptionIds = microsoftOffice365UsersOperations.GetAssignedSubscriptionIds(
				customer.CspId, userName);
			microsoftOffice365UsersOperations.DeleteUser(customer.CspId, userName);

			var customerSubscriptions = microsoftOffice365SubscriptionsOperations.GetSubscriptions(
				customer.CspId);

			var affectedCustomerSubscriptionIds = customerSubscriptions.Select(
				subscription => subscription.Id).Union(userSubscriptionIds).ToList();

			affectedCustomerSubscriptionIds.ForEach(subscriptionId =>
				microsoftOffice365SubscriptionsOperations.ChangeSubscriptionQuantity(
				customer.CspId,
				subscriptionId,
				customerSubscriptions.First(subscription => subscription.Id == subscriptionId)
					.NumberOfAssignedLicenses));
		}
	}
}