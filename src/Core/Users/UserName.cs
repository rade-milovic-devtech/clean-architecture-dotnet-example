using System;

namespace Office365.UserManagement.Core.Users
{
	public sealed class UserName
	{
		private readonly string value;

		public UserName(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException($"{nameof(UserName)} value cannot be empty.", nameof(value));

			this.value = value;
		}

		public override bool Equals(object obj)
		{
			var other = obj as UserName;

			if (ReferenceEquals(other, null)) return false;
			if (GetType() != obj.GetType()) return false;

			return string.Equals(value, other.value, StringComparison.InvariantCultureIgnoreCase);
		}

		public override int GetHashCode() => value.GetHashCode();

		public override string ToString() => value;

		public static bool operator ==(UserName left, UserName right)
		{
			if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;
			if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

			return left.Equals(right);
		}

		public static bool operator !=(UserName left, UserName right) =>
			!(left == right);
	}
}