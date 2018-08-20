using System;

namespace Office365.UserManagement.Core.Customers
{
	public class Customer
	{
		private readonly CustomerNumber number;

		public Customer(
			CustomerNumber number,
			CustomerCspId cspId,
			CustomerLicensingMode licensingMode)
		{
			this.number = number
				?? throw new ArgumentNullException(nameof(number));
			CspId = cspId
				?? throw new ArgumentNullException(nameof(cspId));
			LicensingMode = licensingMode;
		}

		public CustomerCspId CspId { get; }
		public CustomerLicensingMode LicensingMode { get; }

		public override string ToString() =>
			$"{nameof(Customer)} {{ {nameof(number)}: {number}, {nameof(CspId)}: {CspId}, {nameof(LicensingMode)}: {LicensingMode} }}";
	}
}