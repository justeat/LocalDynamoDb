using LocalDynamoDb.Builder;
using LocalDynamoDb.Builder.Docker;
using LocalDynamoDb.Tests.Fixtures;
using Shouldly;
using Xunit;

namespace LocalDynamoDb.Tests
{
    public class DockerDynamoTests : IClassFixture<DockerTestFixture>
    {
        private readonly DockerTestFixture _fixture;

        public DockerDynamoTests(DockerTestFixture fixture)
        {
            _fixture = fixture;
            /*ICanCreateClient builder2 = new LocalDynamoDbBuilder().JarBinaries().InPath("path").OnPort(8001);
            AmazonDynamoDBClient client2 = builder2.CreateClient();*/
        }
                
        [Fact]
        public void ShouldStart()
        {
            
        }
    }

}