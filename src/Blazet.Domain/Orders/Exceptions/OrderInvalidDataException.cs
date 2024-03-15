namespace Blazet.Domain.Orders.Exceptions
{
    public class OrderInvalidDataException : Exception
    {
        public OrderInvalidDataException(string message) : base($"Order invalid data: {message}")
        {
        }
    }
}
