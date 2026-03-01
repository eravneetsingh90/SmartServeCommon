namespace SmartServe.Domain.Constants
{
	public static class ResultCodes
	{
		public const string Success = "R00";
		public const string DataValidationError = "R01";
		public const string Error = "R03";
		public const string DuplicateNotAllowed = "R04";
		public const string ActiveOrderExists = "R05";
	}
	public static class ResultMessages
	{
		public const string Success = "Success";
		public const string DataValidationError = "Data Validation Error";
		public const string Error = "Technical Error";
		public const string DuplicateNotAllowed = "Duplicate Not Allowed";
		public const string ActiveOrderExists = "Active Order Exists";
	}

    public static class Annotations
    {
        public const string ResultCode = "ResultCode";
        public const string ResultMessage = "ResultMessages";
    }
    
}
