using Blazet.Application.Orders.Commands;
using FluentAssertions;
using FluentValidation;

namespace Application.IntegrationTests.Orders.Commands
{
    using static TestsSetup;

    public class AddOrderCommandTest : TestBase
    {
        [Test]
        public void AddOrder_ThrowValidationError()
        {
            var command = new AddOrderCommand(-11, -2);
             FluentActions.Invoking( () =>
                 SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }
    }
}