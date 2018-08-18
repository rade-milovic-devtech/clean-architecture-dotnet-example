using Office365.UserManagement.Core.Customers;

namespace Office365.UserManagement.Core.Users
{
	public interface IOperateOnMicrosoftOffice365Users
	{
		void DeleteUserWith(CustomerCspId customerCspId, UserName userName);
	}
}