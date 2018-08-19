using Office365.UserManagement.Core.Subscriptions;

namespace Office365.UserManagement.Subscriptions
{
	internal class CspSubscriptionBuilder
	{
		public static CspSubscriptionBuilder ACspSubscription => new CspSubscriptionBuilder();

		private string id = string.Empty;
		private int numberOfAvailableLicenses = 1;
		private int numberOfAssignedLicenses;
		private int minAllowedNumberOfAvailableLicenses = 1;
		private int maxAllowedNumberOfAvailableLicenses = 1_000_000_000;

		private CspSubscriptionBuilder() {}

		public CspSubscriptionBuilder WithId(string id)
		{
			this.id = id;

			return this;
		}

		public CspSubscriptionBuilder WithAvailableLicensesOf(int amount)
		{
			numberOfAvailableLicenses = amount;

			return this;
		}

		public CspSubscriptionBuilder WithAssignedLicensesOf(int amount)
		{
			numberOfAssignedLicenses = amount;

			return this;
		}

		public CspSubscriptionBuilder WithMinAllowedLicensesOf(int amount)
		{
			minAllowedNumberOfAvailableLicenses = amount;

			return this;
		}

		public CspSubscriptionBuilder WithMaxAllowedLicensesOf(int amount)
		{
			maxAllowedNumberOfAvailableLicenses = amount;

			return this;
		}

		public CspSubscription Build() =>
			new CspSubscription(
				new SubscriptionCspId(id),
				new LicenseQuantity(numberOfAvailableLicenses),
				new LicenseQuantity(numberOfAssignedLicenses),
				new LicenseQuantity(minAllowedNumberOfAvailableLicenses),
				new LicenseQuantity(maxAllowedNumberOfAvailableLicenses));

		public static implicit operator CspSubscription(CspSubscriptionBuilder builder) => builder.Build();
	}
}