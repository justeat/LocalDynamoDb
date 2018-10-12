using LocalDynamoDb.Builder;
using LocalDynamoDb.Builder.Docker;
using Shouldly;
using Xunit;

namespace LocalDynamoDb.Tests
{
    public class BuilderTests
    {
       
        [Fact]
        public void CanBuildDockerDynamoInstance()
        {
            var builder1 = new LocalDynamoDbBuilder().Container().UsingImage("amazon/dynamodb-local").ExposePort(8000);
            var instance1 = builder1.Build();
            instance1.CreateClient();
            
            instance1.ShouldNotBeNull();
            instance1.ShouldBeAssignableTo<DynamoDockerInstance>();
        }
        
        [Fact]
        public void ConfigurationShouldBeCorrect()
        {
            var image = "amazon/dynamodb-local";
            var port = 8000;
            var containerName = "docker-dynamo-local";
            
            var builder1 = new LocalDynamoDbBuilder().Container().UsingImage(image).ContainerName(() => containerName).ExposePort(port);
            var instance1 = (DynamoDockerInstance)builder1.Build();
            
            instance1.GetImageName().ShouldBe(image);
            instance1.GetPortNumber().ShouldBe(port);
            instance1.GetContainerName().ShouldBe(containerName);
        }
    }
}