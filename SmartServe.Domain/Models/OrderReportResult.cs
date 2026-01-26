namespace SmartServe.Domain.Models
{
	public class OrderReportResult
	{
		public int TotalOrders { get; set; }
		public decimal TotalSales { get; set; }
		public decimal CashSales { get; set; }
		public decimal UpiSales { get; set; }
		public List<Order> Orders { get; set; }
	}

}
