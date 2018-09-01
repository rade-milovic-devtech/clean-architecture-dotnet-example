using System;

namespace Office365.UserManagement.Core.Customers
{
	public sealed class CustomerNumber
	{
		private readonly string value;

		public CustomerNumber(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException($"{nameof(CustomerNumber)} {nameof(value)} cannot be empty.", nameof(value));

			this.value = value;
		}

		public override bool Equals(object obj)
		{
			var other = obj as CustomerNumber;

			if (ReferenceEquals(other, null)) return false;
			if (GetType() != obj.GetType()) return false;

			return string.Equals(value, other.value, StringComparison.InvariantCultureIgnoreCase);
		}

		public override int GetHashCode() => value.GetHashCode();

		public override string ToString() => value;

		public static bool operator ==(CustomerNumber left, CustomerNumber right)
		{
			if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;
			if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

			return left.Equals(right);
		}

		public static bool operator !=(CustomerNumber left, CustomerNumber right) =>
			!(left == right);

		public static implicit operator string(CustomerNumber customerNumber) => customerNumber.value;
	}
}