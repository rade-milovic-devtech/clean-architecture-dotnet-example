namespace Office365.UserManagement.Core.Subscriptions
{
	public static class LicenseQuantityBuilder
	{
		public static LicenseQuantity LicenseQuantityOf(int value) => new LicenseQuantity(value);
	}
}