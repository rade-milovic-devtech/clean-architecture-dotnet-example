using Office365.UserManagement.Core.Subscriptions;

namespace Office365.UserManagement.Subscriptions
{
	public static class LicenseQuantityBuilder
	{
		public static LicenseQuantity LicenseQuantityOf(int value) => new LicenseQuantity(value);
	}
}