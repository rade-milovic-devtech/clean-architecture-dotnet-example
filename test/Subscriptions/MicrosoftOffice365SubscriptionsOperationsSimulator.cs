using Moq;
using Office365.UserManagement.Core.Customers;
using Office365.UserManagement.Core.Subscriptions;

namespace Office365.UserManagement.Subscriptions
{
	public class MicrosoftOffice365SubscriptionsOperationsSimulator
		: IOperateOnMicrosoftOffice365Subscriptions
	{
		private string customerId = string.Empty;

		private readonly Mock<IOperateOnMicrosoftOffice365Subscriptions> microsoftOffice365SubscriptionsOperationsMock =
			new Mock<IOperateOnMicrosoftOffice365Subscriptions>();

		public MicrosoftOffice365SubscriptionsOperationsSimulator ForCustomerWithId(string customerId)
		{
			this.customerId = customerId;

			return this;
		}

		public MicrosoftOffice365SubscriptionsOperationsSimulator ReturnsSubscriptions(params CspSubscription[] subscriptions)
		{
			microsoftOffice365SubscriptionsOperationsMock.Setup(microsoftOffice365SubscriptionsOperations =>
				microsoftOffice365SubscriptionsOperations.GetSubscriptions(new CustomerCspId(customerId)))
					.Returns(new CspSubscriptions(subscriptions));

			return this;
		}

		public CspSubscriptions GetSubscriptions(CustomerCspId customerId) =>
			microsoftOffice365SubscriptionsOperationsMock.Object.GetSubscriptions(customerId);

		public void ChangeSubscriptionQuantity(
			CustomerCspId customerId, SubscriptionCspId subscriptionId, LicenseQuantity newQuantity)
		{
			microsoftOffice365SubscriptionsOperationsMock.Object
				.ChangeSubscriptionQuantity(customerId, subscriptionId, newQuantity);
		}

		public void HasChangedSubscriptionQuantityFor(
			string customerId, string subscriptionId, int newQuantity)
		{
			microsoftOffice365SubscriptionsOperationsMock.Verify(microsoftOffice365SubscriptionsOperations =>
				microsoftOffice365SubscriptionsOperations.ChangeSubscriptionQuantity(
					new CustomerCspId(customerId),
					new SubscriptionCspId(subscriptionId),
					new LicenseQuantity(newQuantity)),
				Times.Once);
		}

		public void HasNotChangedAnySubscriptionQuantities()
		{
			microsoftOffice365SubscriptionsOperationsMock.Verify(microsoftOffice365SubscriptionsOperations =>
				microsoftOffice365SubscriptionsOperations.ChangeSubscriptionQuantity(
					It.IsAny<CustomerCspId>(),
					It.IsAny<SubscriptionCspId>(),
					It.IsAny<LicenseQuantity>())
				, Times.Never);
		}
	}
}