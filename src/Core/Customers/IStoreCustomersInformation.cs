namespace Office365.UserManagement.Core.Customers
{
	public interface IStoreCustomersInformation
	{
		Customer Get(CustomerNumber customerNumber);
	}
}