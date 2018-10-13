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
            var builder1 = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort(8000);
            IDynamoInstance instance1 = builder1.Build();
            instance1.CreateClient();
            
            instance1.ShouldNotBeNull();
            instance1.ShouldBeAssignableTo<DynamoDockerInstance>();
        }
        
        [Fact]
        public void DockerConfigurationAreSet()
        {
            var image = "amazon/dynamodb-local";
            var port = 8000;
            var containerName = "docker-dynamo-local";
            
            var builder1 = new LocalDynamoDbBuilder().Container().UsingCustomImage(image).ContainerName(containerName).ExposePort(port);
            var instance1 = (DynamoDockerInstance)builder1.Build();
            
            instance1.ImageName.ShouldBe(image);
            instance1.PortNumber.ShouldBe(port);
            instance1.ContainerName.ShouldBe(containerName);
        }
    }
}