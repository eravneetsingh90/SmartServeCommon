namespace SmartServe.Domain.Constants
{
	public static class ResultCodes
	{
		public const string Success = "R00";
		public const string DataValidationError = "R01";
		public const string Error = "R03";
		public const string DuplicateNotAllowed = "R04";
	}
	public static class ResultMessages
	{
		public const string Success = "Success";
		public const string DataValidationError = "Data Validation Error";
		public const string Error = "Technical Error";
		public const string DuplicateNotAllowed = "Duplicate Not Allowed";
	}
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
	public static class StockItem
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
}
