namespace Office365.UserManagement.Core.Users
{
	public interface IPerformUserOperations
	{
		void GetUserDetails(GetUserDetailsCommand command, IPresentUserDetails presenter);
		void DeleteUser(DeleteUserCommand command);
	}
}