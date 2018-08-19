using System;

namespace Office365.UserManagement.Core.Subscriptions
{
	public class CspSubsctriptionAvailableLicenseNumberAlignmentResult
	{
		private readonly LicenseQuantity originalNumberOfAvailableLicenses;

		public CspSubsctriptionAvailableLicenseNumberAlignmentResult(
			SubscriptionCspId subscriptionId,
			LicenseQuantity originalNumberOfAvailableLicenses,
			LicenseQuantity newNumberOfAvailableLicenses)
		{
			SubscriptionId = subscriptionId
				?? throw new ArgumentNullException(nameof(subscriptionId));
			this.originalNumberOfAvailableLicenses = originalNumberOfAvailableLicenses
				?? throw new ArgumentNullException(nameof(originalNumberOfAvailableLicenses));
			NewNumberOfAvailableLicenses = newNumberOfAvailableLicenses
				?? throw new ArgumentNullException(nameof(newNumberOfAvailableLicenses));
		}

		public SubscriptionCspId SubscriptionId { get; }
		public LicenseQuantity NewNumberOfAvailableLicenses { get; }

		public bool RequiresUpdateOfAvailableLicensesForSubscription =>
			originalNumberOfAvailableLicenses != NewNumberOfAvailableLicenses;
	}
}