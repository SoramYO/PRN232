namespace PRN232.Lab1.ProductStore.Service.Payload.Request
{
	public class ProductRequest
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public int CategoryId { get; set; }
		public short? UnitsInStock { get; set; }
		public decimal? UnitPrice { get; set; }
	}
}
