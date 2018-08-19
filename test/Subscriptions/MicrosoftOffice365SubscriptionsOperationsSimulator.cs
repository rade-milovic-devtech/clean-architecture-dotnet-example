using Moq;
using Office365.UserManagement.Core.Customers;
using Office365.UserManagement.Core.Subscriptions;
using System.Collections.Generic;

namespace Office365.UserManagement.Subscriptions
{
	internal class MicrosoftOffice365SubscriptionsOperationsSimulator
		: IOperateOnMicrosoftOffice365Subscriptions
	{
		private string customerCspId = string.Empty;

		private readonly Mock<IOperateOnMicrosoftOffice365Subscriptions> microsoftOffice365SubscriptionsOperationsMock =
			new Mock<IOperateOnMicrosoftOffice365Subscriptions>();

		public MicrosoftOffice365SubscriptionsOperationsSimulator ForCustomerWithCspId(string customerCspId)
		{
			this.customerCspId = customerCspId;

			return this;
		}

		public MicrosoftOffice365SubscriptionsOperationsSimulator ReturnsSubscriptions(params Subscription[] subscriptions)
		{
			microsoftOffice365SubscriptionsOperationsMock.Setup(microsoftOffice365SubscriptionsOperations =>
				microsoftOffice365SubscriptionsOperations.GetSubscriptions(new CustomerCspId(customerCspId)))
					.Returns(subscriptions);

			return this;
		}

		public IEnumerable<Subscription> GetSubscriptions(CustomerCspId customerCspId) =>
			microsoftOffice365SubscriptionsOperationsMock.Object.GetSubscriptions(customerCspId);

		public void ChangeSubscriptionQuantity(CustomerCspId customerCspId,
			SubscriptionCspId subscriptionCspId, LicenseQuantity newQuantity)
		{
			microsoftOffice365SubscriptionsOperationsMock.Object
				.ChangeSubscriptionQuantity(customerCspId, subscriptionCspId, newQuantity);
		}

		public void HasChangedSubscriptionQuantityFor(
			string customerCspId, string subscriptionCspId, int newQuantity)
		{
			microsoftOffice365SubscriptionsOperationsMock.Verify(microsoftOffice365SubscriptionsOperations =>
				microsoftOffice365SubscriptionsOperations.ChangeSubscriptionQuantity(
					new CustomerCspId(customerCspId),
					new SubscriptionCspId(subscriptionCspId),
					new LicenseQuantity(newQuantity)),
				Times.Once);
		}
	}
}