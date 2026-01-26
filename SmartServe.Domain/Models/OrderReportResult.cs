namespace SmartServe.Domain.Models
{
	public class OrderReportResult
	{
		public int TotalOrders { get; set; }
		public decimal TotalSales { get; set; }
		public List<Order> Orders { get; set; }
	}

}
