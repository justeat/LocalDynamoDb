using System.Threading.Tasks;
using Xunit;

namespace LocalDynamoDb.Tests
{
    public class DockerIntegrationTest : IClassFixture<IntegrationFixture>
    {
        private readonly IntegrationFixture _fixture;

        public DockerIntegrationTest(IntegrationFixture fixture)
        {
            _fixture = fixture;
        }
        
        [Fact]
        public void FullIntegrationTest()
        {
            _fixture.Start();
        }
    }
}