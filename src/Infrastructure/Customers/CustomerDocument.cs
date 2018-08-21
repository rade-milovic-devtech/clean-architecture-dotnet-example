using MongoDB.Bson.Serialization.Attributes;

namespace Office365.UserManagement.Infrastructure.Customers
{
	[BsonIgnoreExtraElements]
	public class CustomerDocument
	{
		public string Number { get; set; }
		public string CspId { get; set; }
		public string LicensingMode { get; set; }
	}
}