using System;
using System.Collections.Generic;
using System.Linq;

namespace Office365.UserManagement.Core.Subscriptions
{
	public class CspSubscriptions
	{
		public CspSubscriptions(IEnumerable<CspSubscription> items)
		{
			Items = items
				?? throw new ArgumentNullException(nameof(items));
		}

		public IEnumerable<CspSubscription> Items { get; }

		public CspSubscriptions OnlyWith(IEnumerable<SubscriptionCspId> ids) =>
			new CspSubscriptions(Items.Where(item => ids.Contains(item.Id)));

		public IEnumerable<CspSubsctriptionAvailableLicenseNumberAlignmentResult> AlignNumberOfAvailableAndAssignedLicenses() =>
			Items.Select(item => item.AlignNumberOfAvailableAndAssignedLicenses())
				.Where(result => result.RequiresUpdateOfAvailableLicensesForSubscription);
	}
}