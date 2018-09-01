using System;

namespace Office365.UserManagement.Core.Users
{
	public class User
	{
		public User(
			UserName userName,
			string firstName,
			string lastName)
		{
			UserName = userName
				?? throw new ArgumentNullException(nameof(userName));
			if (string.IsNullOrWhiteSpace(firstName))
				throw new ArgumentException($"{nameof(User)} {nameof(firstName)} cannot be empty.", nameof(firstName));
			FirstName = firstName;
			if (string.IsNullOrWhiteSpace(lastName))
				throw new ArgumentException($"{nameof(User)} {nameof(lastName)} cannot be empty.", nameof(lastName));
			LastName = lastName;
		}

		public UserName UserName { get; }
		public string FirstName { get; }
		public string LastName { get; }

		public override string ToString() =>
			$"{nameof(User)} {{ {nameof(UserName)}: {UserName}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName} }}";
	}
}