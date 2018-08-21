namespace Office365.UserManagement.Core.Users
{
	public interface IPerformUserOperations
	{
		void DeleteUser(DeleteUserCommand command);
	}
}