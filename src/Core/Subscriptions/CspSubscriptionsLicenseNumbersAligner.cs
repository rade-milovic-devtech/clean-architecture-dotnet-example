using System;
using System.Collections.Generic;
using System.Linq;
using Office365.UserManagement.Core.Customers;

namespace Office365.UserManagement.Core.Subscriptions
{
	public class CspSubscriptionsLicenseNumbersAligner
	{
		private readonly CspSubscriptions cspSubscriptions;
		private readonly CustomerLicensingMode customerLicensingMode;

		public CspSubscriptionsLicenseNumbersAligner(
			CspSubscriptions cspSubscriptions,
			CustomerLicensingMode customerLicensingMode)
		{
			this.cspSubscriptions = cspSubscriptions
				?? throw new ArgumentNullException(nameof(cspSubscriptions));
			this.customerLicensingMode = customerLicensingMode;
		}

		public IEnumerable<CspSubsctriptionAvailableLicenseNumberAlignmentResult> AlignLicenseNumbersForCspSubscriptionsWithIdsOf(
			IEnumerable<SubscriptionCspId> subscriptionIds)
		{
			if (customerLicensingMode == CustomerLicensingMode.Manual)
				return Enumerable.Empty<CspSubsctriptionAvailableLicenseNumberAlignmentResult>();

			return cspSubscriptions.OnlyWith(subscriptionIds)
				.AlignNumberOfAvailableAndAssignedLicenses();
		}
	}
}