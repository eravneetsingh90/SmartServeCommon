namespace SmartServe.Domain.Constants
{
	public static class TableStatusCodes
	{
		public const string BLANK = "BLANK";
		public const string RUNNING = "RUNNING";
		public const string PRINTED = "PRINTED";
		public const string PAID = "PAID";
		public const string RUNNING_KOT = "RUNNING_KOT";
	}
	public static class PaymentMode
	{
		public const string CASH = "CASH";
		public const string UPI = "UPI";
		public const string CARD = "CARD";
		public const string PART = "PART";
	}
	public static class StockMode
	{
		public const string NONE = "NONE";
		public const string SEALED = "SEALED";
		public const string INGREDIENT = "INGREDIENT";
	}
}
