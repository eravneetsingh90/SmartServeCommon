namespace SmartServe.Common.Models
{
    public class Log
    {
        public string Action { get; set; }
        public int? ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
