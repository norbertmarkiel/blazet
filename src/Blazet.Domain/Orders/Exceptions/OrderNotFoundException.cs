namespace Blazet.Domain.Orders.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException() : base("Zamówienie nie zostało znalezione")
        {
        }
    }
}
