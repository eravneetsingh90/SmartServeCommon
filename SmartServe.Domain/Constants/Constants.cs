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
	public static class StockItemType
	{
		public const string VARIANT = "VARIANT";
		public const string INGREDIENT = "INGREDIENT";
	}
	public static class StockTxnType
	{
		public const string IN = "IN";
		public const string OUT = "OUT";
		public const string ADJUST = "ADJUST";
	}
	public static class DiscountType
	{
		public const string PERCENT = "PERCENT";
		public const string FLAT = "FLAT";
	}
	public static class UnitType
	{
		public const string PCS = "PCS";
		public const string ML = "ML";
		public const string GM = "GM";
	}
}
