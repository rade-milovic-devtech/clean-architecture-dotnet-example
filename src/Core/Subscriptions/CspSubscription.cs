using System;

namespace Office365.UserManagement.Core.Subscriptions
{
	public class CspSubscription
	{
		private readonly LicenseQuantity numberOfAssignedLicenses;
		private readonly LicenseQuantity minAllowedNumberOfAvailableLicenses;
		private readonly LicenseQuantity maxAllowedNumberOfAvailableLicenses;

		public CspSubscription(
			SubscriptionCspId id,
			LicenseQuantity numberOfAvailableLicenses,
			LicenseQuantity numberOfAssignedLicenses,
			LicenseQuantity minAllowedNumberOfAvailableLicenses,
			LicenseQuantity maxAllowedNumberOfAvailableLicenses)
		{
			Id = id
				?? throw new ArgumentNullException(nameof(id));
			NumberOfAvailableLicenses = numberOfAvailableLicenses
				?? throw new ArgumentNullException(nameof(numberOfAvailableLicenses));
			this.numberOfAssignedLicenses = numberOfAssignedLicenses
				?? throw new ArgumentNullException(nameof(numberOfAssignedLicenses));
			this.minAllowedNumberOfAvailableLicenses = minAllowedNumberOfAvailableLicenses
				?? throw new ArgumentNullException(nameof(minAllowedNumberOfAvailableLicenses));
			this.maxAllowedNumberOfAvailableLicenses = maxAllowedNumberOfAvailableLicenses
				?? throw new ArgumentNullException(nameof(maxAllowedNumberOfAvailableLicenses));
		}

		public SubscriptionCspId Id { get; }
		public LicenseQuantity NumberOfAvailableLicenses { get; }

		public CspSubsctriptionAvailableLicenseNumberAlignmentResult AlignNumberOfAvailableAndAssignedLicenses()
		{
			LicenseQuantity newNumberOfAvailableLicenses;
			if (numberOfAssignedLicenses < minAllowedNumberOfAvailableLicenses)
				newNumberOfAvailableLicenses = new LicenseQuantity(minAllowedNumberOfAvailableLicenses);
			else if (numberOfAssignedLicenses > maxAllowedNumberOfAvailableLicenses)
				newNumberOfAvailableLicenses = new LicenseQuantity(maxAllowedNumberOfAvailableLicenses);
			else
				newNumberOfAvailableLicenses = new LicenseQuantity(numberOfAssignedLicenses);

			return new CspSubsctriptionAvailableLicenseNumberAlignmentResult(
				Id,
				NumberOfAvailableLicenses,
				newNumberOfAvailableLicenses);
		}

		public override bool Equals(object obj)
		{
			var other = obj as CspSubscription;

			if (ReferenceEquals(other, null)) return false;
			if (GetType() != obj.GetType()) return false;

			return Id == other.Id;
		}

		public override int GetHashCode() => Id.GetHashCode();

		public override string ToString() =>
			$"{nameof(CspSubscription)} {{ {nameof(Id)}: {Id}, {nameof(NumberOfAvailableLicenses)}: {NumberOfAvailableLicenses}, {nameof(numberOfAssignedLicenses)}: {numberOfAssignedLicenses}, {nameof(minAllowedNumberOfAvailableLicenses)}: {minAllowedNumberOfAvailableLicenses}, {nameof(maxAllowedNumberOfAvailableLicenses)}: {maxAllowedNumberOfAvailableLicenses} }}";

		public static bool operator ==(CspSubscription left, CspSubscription right)
		{
			if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;
			if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

			return left.Equals(right);
		}

		public static bool operator !=(CspSubscription left, CspSubscription right) =>
			!(left == right);
	}
}