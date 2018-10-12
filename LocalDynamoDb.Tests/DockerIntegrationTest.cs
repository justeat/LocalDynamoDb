using System.Threading.Tasks;
using Xunit;

namespace LocalDynamoDb.Tests
{
    public class DockerIntegrationTest : IClassFixture<IntegrationFixture>
    {
        private readonly IntegrationFixture _fixture;
        /*private LocalDynamo _dynamo;*/

        public DockerIntegrationTest(IntegrationFixture fixture)
        {
            _fixture = fixture;
            /*_dynamo = new LocalDynamo("amazon/dynamodb-local", "je-dynamotest", 8000);
            _dynamo.Start();*/
        }
        
        [Fact]
        public void FullIntegrationTest()
        {
            
            _fixture.Start();
        }
    }
}