namespace Office365.UserManagement.Core.Customers
{
	public interface IStoreCustomerInformation
	{
		CustomerCspId GetCspIdFor(CustomerNumber customerNumber);
	}
}