namespace Blazet.Application.Orders.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string InternalNumber { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
