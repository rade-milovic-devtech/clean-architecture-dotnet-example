using System;

namespace Office365.UserManagement.Core.Customers
{
	public class Customer
	{
		public Customer(
			CustomerNumber number,
			CustomerCspId cspId,
			CustomerLicensingMode licensingMode)
		{
			Number = number
				?? throw new ArgumentNullException(nameof(number));
			CspId = cspId
				?? throw new ArgumentNullException(nameof(cspId));
			LicensingMode = licensingMode;
		}

		public CustomerNumber Number { get; }
		public CustomerCspId CspId { get; }
		public CustomerLicensingMode LicensingMode { get; }

		public override string ToString() =>
			$"{nameof(Customer)} {{ {nameof(Number)}: {Number}, {nameof(CspId)}: {CspId}, {nameof(LicensingMode)}: {LicensingMode} }}";
	}
}