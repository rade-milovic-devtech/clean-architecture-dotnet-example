namespace Office365.UserManagement.Core.Customers
{
	public interface IReadCustomerConfiguration
	{
		CustomerLicensingMode GetLicensingModeFor(CustomerNumber customerNumber);
	}
}