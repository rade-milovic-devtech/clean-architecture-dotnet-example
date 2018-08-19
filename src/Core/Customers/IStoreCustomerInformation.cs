namespace Office365.UserManagement.Core.Customers
{
	public interface IStoreCustomerInformation
	{
		Customer Get(CustomerNumber customerNumber);
	}
}