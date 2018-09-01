using System;

namespace Office365.UserManagement.Core.Subscriptions
{
	public sealed class LicenseQuantity
	{
		private readonly int value;

		public LicenseQuantity(int value)
		{
			if (value < 0)
				throw new ArgumentException($"{nameof(LicenseQuantity)} {nameof(value)} cannot be negative.", nameof(value));

			this.value = value;
		}

		public override bool Equals(object obj)
		{
			var other = obj as LicenseQuantity;

			if (ReferenceEquals(other, null)) return false;
			if (GetType() != obj.GetType()) return false;

			return value == other.value;
		}

		public override int GetHashCode() => value.GetHashCode();

		public override string ToString() => value.ToString();

		public static bool operator ==(LicenseQuantity left, LicenseQuantity right)
		{
			if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;
			if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

			return left.Equals(right);
		}

		public static bool operator !=(LicenseQuantity left, LicenseQuantity right) =>
			!(left == right);

		public static bool operator <(LicenseQuantity left, LicenseQuantity right) =>
			left.value < right.value;

		public static bool operator >(LicenseQuantity left, LicenseQuantity right) =>
			left.value > right.value;

		public static implicit operator int(LicenseQuantity licenseQuantity) => licenseQuantity.value;
	}
}