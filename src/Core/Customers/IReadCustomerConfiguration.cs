namespace Office365.UserManagement.Core.Customers
{
	public interface IReadCustomerConfiguration
	{
		CustomerLicensingMode GetLicensingMode(CustomerNumber customerNumber);
	}
}