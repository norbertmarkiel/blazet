
namespace Blazet.Domain.Orders.Exceptions

{
    public class OrderInvalidDataException : System.Exception
    {
        public OrderInvalidDataException(string message) : base($"Order invalid data: {message}")
        {
        }
    }
}
