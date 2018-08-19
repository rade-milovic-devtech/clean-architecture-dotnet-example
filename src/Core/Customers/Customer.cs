using System;

namespace Office365.UserManagement.Core.Customers
{
	public class Customer
	{
		private readonly CustomerNumber number;
		private readonly CustomerCspId cspId;

		public Customer(
			CustomerNumber number,
			CustomerCspId cspId)
		{
			this.number = number
				?? throw new ArgumentNullException(nameof(number));
			this.cspId = cspId
				?? throw new ArgumentNullException(nameof(cspId));
		}

		public CustomerCspId CspId => cspId;

		public override bool Equals(object obj)
		{
			var other = obj as Customer;

			if (ReferenceEquals(other, null)) return false;
			if (GetType() != obj.GetType()) return false;

			return number == other.number;
		}

		public override int GetHashCode() => number.GetHashCode();

		public override string ToString() =>
			$"{nameof(Customer)} {{ {nameof(number)}: {number}, {nameof(cspId)}: {cspId} }}";

		public static bool operator ==(Customer left, Customer right)
		{
			if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;
			if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

			return left.Equals(right);
		}

		public static bool operator !=(Customer left, Customer right) =>
			!(left == right);
	}
}