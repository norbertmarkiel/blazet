namespace Application.IntegrationTests
{
    using static TestsSetup;
    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}