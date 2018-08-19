using Office365.UserManagement.Core.Subscriptions;

namespace Office365.UserManagement.Subscriptions
{
	internal class SubscriptionBuilder
	{
		public static SubscriptionBuilder ASubscription => new SubscriptionBuilder();

		private string id = string.Empty;
		private int numberOfAvailableLicenses = 1;
		private int numberOfAssignedLicenses;
		private int minAllowedNumberOfAvailableLicenses = 1;
		private int maxAllowedNumberOfAvailableLicenses = 1_000_000_000;

		private SubscriptionBuilder() {}

		public SubscriptionBuilder WithId(string id)
		{
			this.id = id;

			return this;
		}

		public SubscriptionBuilder WithAvailableLicensesOf(int amount)
		{
			numberOfAvailableLicenses = amount;

			return this;
		}

		public SubscriptionBuilder WithAssignedLicensesOf(int amount)
		{
			numberOfAssignedLicenses = amount;

			return this;
		}

		public SubscriptionBuilder WithMinAllowedLicensesOf(int amount)
		{
			minAllowedNumberOfAvailableLicenses = amount;

			return this;
		}

		public SubscriptionBuilder WithMaxAllowedLicensesOf(int amount)
		{
			maxAllowedNumberOfAvailableLicenses = amount;

			return this;
		}

		public Subscription Build() =>
			new Subscription(
				new SubscriptionCspId(id),
				new LicenseQuantity(numberOfAvailableLicenses),
				new LicenseQuantity(numberOfAssignedLicenses),
				new LicenseQuantity(minAllowedNumberOfAvailableLicenses),
				new LicenseQuantity(maxAllowedNumberOfAvailableLicenses));

		public static implicit operator Subscription(SubscriptionBuilder builder) => builder.Build();
	}
}