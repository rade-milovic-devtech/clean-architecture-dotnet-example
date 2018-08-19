using System;

namespace Office365.UserManagement.Core.Customers
{
	public class Customer
	{
		private readonly CustomerNumber number;
		private readonly CustomerLicensingMode licensingMode;

		public Customer(
			CustomerNumber number,
			CustomerCspId cspId,
			CustomerLicensingMode licensingMode)
		{
			this.number = number
				?? throw new ArgumentNullException(nameof(number));
			CspId = cspId
				?? throw new ArgumentNullException(nameof(cspId));
			this.licensingMode = licensingMode;
		}

		public CustomerCspId CspId { get; }

		public override string ToString() =>
			$"{nameof(Customer)} {{ {nameof(number)}: {number}, {nameof(CspId)}: {CspId}, {nameof(licensingMode)}: {licensingMode} }}";
	}
}