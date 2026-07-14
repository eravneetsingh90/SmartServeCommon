namespace SmartServe.Common.Models
{
    public class RequestLogContext
    {
        public string Action { get; set; }
        public int? StatusCode { get; set; }
        public string ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public List<string> Errors { get; set; } = new();

        public int? UserId { get; set; }
    }
}
