namespace Blazet.Domain.Orders.Entities
{
    public class Order
    {
        public Order(string internalNumber, int quantity, decimal price)
        {
            Id = new Guid();
            Quantity = quantity;
            InternalNumber = internalNumber;
            Price = price;
        }

        public Guid Id { get; private set; }
        public string InternalNumber { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public void Update(int quantity, decimal price)
        {
            Quantity = quantity;
            Price = price;
        }
    }
}
