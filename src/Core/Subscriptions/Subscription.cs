using System;

namespace Office365.UserManagement.Core.Subscriptions
{
	public class Subscription
	{
		private readonly SubscriptionCspId id;
		private readonly LicenseQuantity numberOfAvailableLicenses;
		private readonly LicenseQuantity numberOfAssignedLicenses;
		private readonly LicenseQuantity minAllowedNumberOfAvailableLicenses;
		private readonly LicenseQuantity maxAllowedNumberOfAvailableLicenses;

		public Subscription(
			SubscriptionCspId id,
			LicenseQuantity numberOfAvailableLicenses,
			LicenseQuantity numberOfAssignedLicenses,
			LicenseQuantity minAllowedNumberOfAvailableLicenses,
			LicenseQuantity maxAllowedNumberOfAvailableLicenses)
		{
			this.id = id
				?? throw new ArgumentNullException(nameof(id));
			this.numberOfAvailableLicenses = numberOfAvailableLicenses
				?? throw new ArgumentNullException(nameof(numberOfAvailableLicenses));
			this.numberOfAssignedLicenses = numberOfAssignedLicenses
				?? throw new ArgumentNullException(nameof(numberOfAssignedLicenses));
			this.minAllowedNumberOfAvailableLicenses = minAllowedNumberOfAvailableLicenses
				?? throw new ArgumentNullException(nameof(minAllowedNumberOfAvailableLicenses));
			this.maxAllowedNumberOfAvailableLicenses = maxAllowedNumberOfAvailableLicenses
				?? throw new ArgumentNullException(nameof(maxAllowedNumberOfAvailableLicenses));
		}

		public SubscriptionCspId Id => id;

		public LicenseQuantity NumberOfAssignedLicenses => numberOfAssignedLicenses;

		public override bool Equals(object obj)
		{
			var other = obj as Subscription;

			if (ReferenceEquals(other, null)) return false;
			if (GetType() != obj.GetType()) return false;

			return id == other.id;
		}

		public override int GetHashCode() => id.GetHashCode();

		public override string ToString() =>
			$"{nameof(Subscription)} {{ {nameof(id)}: {id}, {nameof(numberOfAvailableLicenses)}: {numberOfAvailableLicenses}, {nameof(numberOfAssignedLicenses)}: {numberOfAssignedLicenses}, {nameof(minAllowedNumberOfAvailableLicenses)}: {minAllowedNumberOfAvailableLicenses}, {nameof(maxAllowedNumberOfAvailableLicenses)}: {maxAllowedNumberOfAvailableLicenses} }}";

		public static bool operator ==(Subscription left, Subscription right)
		{
			if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;
			if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

			return left.Equals(right);
		}

		public static bool operator !=(Subscription left, Subscription right) =>
			!(left == right);
	}
}