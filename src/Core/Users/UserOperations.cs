using Office365.UserManagement.Core.Customers;
using Office365.UserManagement.Core.Subscriptions;
using System.Collections.Generic;
using System.Linq;

namespace Office365.UserManagement.Core.Users
{
	public class UserOperations
	{
		private readonly IStoreCustomerInformation customerInformationStore;
		private readonly IOperateOnMicrosoftOffice365Users microsoftOffice365UsersOperations;
		private readonly IOperateOnMicrosoftOffice365Subscriptions microsoftOffice365SubscriptionsOperations;

		public UserOperations(
			IStoreCustomerInformation customerInformationStore,
			IOperateOnMicrosoftOffice365Users microsoftOffice365UsersOperations,
			IOperateOnMicrosoftOffice365Subscriptions microsoftOffice365SubscriptionsOperations)
		{
			this.customerInformationStore = customerInformationStore;
			this.microsoftOffice365UsersOperations = microsoftOffice365UsersOperations;
			this.microsoftOffice365SubscriptionsOperations = microsoftOffice365SubscriptionsOperations;
		}

		public void DeleteUser(DeleteUserCommand command)
		{
			var customer = GetCustomerInformationFor(command.CustomerNumber);

			var cspSubscriptionIdsOfLicensesAssignedToAUser =
				GetCspSubscriptionIdsOfLicensesAssignedToAUserWith(customer.CspId, command.UserName);
			DeleteUserWith(customer.CspId, command.UserName);

			var customerCspSubscriptions = GetCspSubscriptionsFor(customer.CspId);

			var cspSubsctriptionsLicenseNumbersAlignmentResults =
				AlignNumberOfLicensesForSubscriptionsAffectedByUserDeletion(
					customer.LicensingMode, customerCspSubscriptions, cspSubscriptionIdsOfLicensesAssignedToAUser);

			UpdateNumberOfAvailableLicensesFor(
				customer.CspId, cspSubsctriptionsLicenseNumbersAlignmentResults);
		}

		private Customer GetCustomerInformationFor(string customerNumber) =>
			customerInformationStore.Get(new CustomerNumber(customerNumber));

		private IEnumerable<SubscriptionCspId> GetCspSubscriptionIdsOfLicensesAssignedToAUserWith(
			CustomerCspId customerCspId, string userName) =>
				microsoftOffice365UsersOperations.GetAssignedSubscriptionIds(
					customerCspId, new UserName(userName));

		private void DeleteUserWith(CustomerCspId customerCspId, string userName)
		{
			microsoftOffice365UsersOperations.DeleteUser(customerCspId, new UserName(userName));
		}

		private CspSubscriptions GetCspSubscriptionsFor(CustomerCspId customerCspId) =>
			microsoftOffice365SubscriptionsOperations.GetSubscriptions(customerCspId);

		private IEnumerable<CspSubsctriptionAvailableLicenseNumberAlignmentResult> AlignNumberOfLicensesForSubscriptionsAffectedByUserDeletion(
			CustomerLicensingMode customerLicensingMode, CspSubscriptions customerCspSubscriptions, IEnumerable<SubscriptionCspId> userSubscriptionIds)
		{
			var cspSubscriptionsLicenseNumbersAligner = new CspSubscriptionsLicenseNumbersAligner(
				customerCspSubscriptions, customerLicensingMode);

			return cspSubscriptionsLicenseNumbersAligner
				.AlignLicenseNumbersForCspSubscriptionsWithIdsOf(userSubscriptionIds);
		}

		private void UpdateNumberOfAvailableLicensesFor(
			CustomerCspId customerCspId,
			IEnumerable<CspSubsctriptionAvailableLicenseNumberAlignmentResult> cspSubsctriptionAvailableLicenseNumberAlignmentResults)
		{
			cspSubsctriptionAvailableLicenseNumberAlignmentResults.ToList().ForEach(result =>
				UpdateNumberOfAvailableLicensesFor(customerCspId, result));
		}

		private void UpdateNumberOfAvailableLicensesFor(CustomerCspId customerCspId,
			CspSubsctriptionAvailableLicenseNumberAlignmentResult result)
		{
			microsoftOffice365SubscriptionsOperations.ChangeSubscriptionQuantity(
				customerCspId,
				result.SubscriptionId,
				result.NewNumberOfAvailableLicenses);
		}
	}
}