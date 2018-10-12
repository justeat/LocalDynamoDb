using LocalDynamoDb.Builder;
using LocalDynamoDb.Builder.Docker;
using LocalDynamoDb.Tests.Fixtures;
using Shouldly;
using Xunit;

namespace LocalDynamoDb.Tests
{
    public class DockerDynamoTests
    {
        private readonly IClassFixture<DockerTestFixture> _fixture;

        public DockerDynamoTests(IClassFixture<DockerTestFixture> fixture)
        {
            _fixture = fixture;
            /*ICanCreateClient builder2 = new LocalDynamoDbBuilder().JarBinaries().InPath("path").OnPort(8001);
            AmazonDynamoDBClient client2 = builder2.CreateClient();*/
        }
                
        [Fact]
        public void ShouldStart()
        {
            _fixture.St

            instance1.Start().ShouldBe(true);
            instance1.Stop();
        }
    }

}