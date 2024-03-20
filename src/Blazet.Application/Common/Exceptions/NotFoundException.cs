using System.Net;

namespace Blazet.Application.Common.Exceptions
{
    public class NotFoundException : ServerException
    {
        public NotFoundException(string message)
            : base(message, HttpStatusCode.NotFound)
        {
        }
        public NotFoundException(string name, Guid id)
            : base($"{name} ({id}) was not found.", HttpStatusCode.NotFound)
        {
        }
    }
}
