namespace Office365.UserManagement.Core.Users
{
	public class UserBuilder
	{
		public static UserBuilder AUser => new UserBuilder();

		private string userName = string.Empty;
		private string firstName = string.Empty;
		private string lastName = string.Empty;

		private UserBuilder() {}

		public UserBuilder WithUserName(string userName)
		{
			this.userName = userName;

			return this;
		}

		public UserBuilder WithFirstName(string firstName)
		{
			this.firstName = firstName;

			return this;
		}

		public UserBuilder WithLastName(string lastName)
		{
			this.lastName = lastName;

			return this;
		}

		public User Build() =>
			new User(
				new UserName(userName),
				firstName,
				lastName);

		public static implicit operator User(UserBuilder builder) => builder.Build();
	}
}