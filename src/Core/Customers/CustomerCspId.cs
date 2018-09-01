using System;

namespace Office365.UserManagement.Core.Customers
{
	public sealed class CustomerCspId
	{
		private readonly string value;

		public CustomerCspId(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException($"{nameof(CustomerCspId)} {nameof(value)} cannot be empty.", nameof(value));

			this.value = value;
		}

		public override bool Equals(object obj)
		{
			var other = obj as CustomerCspId;

			if (ReferenceEquals(other, null)) return false;
			if (GetType() != obj.GetType()) return false;

			return string.Equals(value, other.value, StringComparison.InvariantCultureIgnoreCase);
		}

		public override int GetHashCode() => value.GetHashCode();

		public override string ToString() => value;

		public static bool operator ==(CustomerCspId left, CustomerCspId right)
		{
			if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;
			if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

			return left.Equals(right);
		}

		public static bool operator !=(CustomerCspId left, CustomerCspId right) =>
			!(left == right);
	}
}