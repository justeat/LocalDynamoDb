using Amazon.DynamoDBv2;
using LocalDynamoDb.Builder;
using LocalDynamoDb.Builder.Docker;
using Shouldly;
using Xunit;

namespace LocalDynamoDb.Tests.Docker
{
    public class DockerDynamoBuilderTests
    {
        [Fact]
        public void CanBuildDockerDynamoInstance()
        {
            // Arrange
            var builder = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort();
            
            // Act
            var dynamo = builder.Build();

            // Assert
            dynamo.ShouldBeAssignableTo<IDockerDynamoInstance>();
        }

        [Fact]
        public void DefaultImageNameIsSet()
        {
            // Arrange
            var builder = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort();
            
            // Act
            var dynamo = (IDockerDynamoInstance) builder.Build();

            // Assert
            dynamo.ImageName.ShouldBe("amazon/dynamodb-local");
        }

        [Fact]
        public void CustomImageNameIsSet()
        {
            var image = "my-custom-image";

            // Arrange
            var builder = new LocalDynamoDbBuilder().Container().UsingCustomImage(image).ExposePort();
            
            // Act
            var dynamo = (IDockerDynamoInstance) builder.Build();

            // Assert
            dynamo.ImageName.ShouldBe(image);
        }

        [Fact]
        public void DefaultPortIsSet()
        {
            // Arrange
            var builder = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort();
            
            // Act
            var dynamo = (IDockerDynamoInstance) builder.Build();

            // Assert
            dynamo.PortNumber.ShouldBe(8000);
        }

        [Fact]
        public void CustomPortIsSet()
        {
            var customPort = 8001;

            // Arrange
            var builder = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort(customPort);
            
            // Act
            var dynamo = (IDockerDynamoInstance) builder.Build();

            // Assert
            dynamo.PortNumber.ShouldBe(customPort);
        }

        [Fact]
        public void DefaultContainerNameIsSet()
        {
            // Arrange
            var builder = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort();
            
            // Act
            var dynamo = (IDockerDynamoInstance) builder.Build();

            // Assert
            dynamo.ContainerName.ShouldBe("docker-dynamo-local");
        }


        [Fact]
        public void CustomContainerNameIsSet()
        {
            // Arrange
            var builder = new LocalDynamoDbBuilder()
                .Container()
                .UsingDefaultImage()
                .ContainerName("custom-name")
                .ExposePort();
            
            // Act
            var dynamo = (IDockerDynamoInstance) builder.Build();

            // Assert
            dynamo.ContainerName.ShouldBe("custom-name");
        }

        [Fact]
        public void DefaultTagIsLatest()
        {
            // Arrange
            var builder = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort();
            
            // Act
            var dynamo = (IDockerDynamoInstance) builder.Build();

            // Assert
            dynamo.Tag.ShouldBe("latest");
        }

        [Fact]
        public void CustomTagIsSet()
        {
            var tag = "1.11.119";
            
            // Arrange
            var builder = new LocalDynamoDbBuilder().Container().UsingDefaultImage(tag).ExposePort();
            
            // Act
            var dynamo = (IDockerDynamoInstance) builder.Build();

            // Assert
            dynamo.Tag.ShouldBe(tag);
        }
        
        [Fact]
        public void AmazonDynamoDbClientIsCreated()
        {
            // Arrange
            var builder = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort();
            var dynamo = builder.Build();

            // Act
            var client = dynamo.CreateClient();

            // Assert
            client.ShouldBeAssignableTo<AmazonDynamoDBClient>();
        }
                
        [Fact]
        public void AmazonDynamoDbClientIsConfiguredCorrectly()
        {
            // Arrange
            var builder = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort(8001);
            var dynamo = builder.Build();
            
            // Act
            var client = dynamo.CreateClient();

            // Assert
            client.Config.ServiceURL.ShouldBe("http://localhost:8001");
        }
    }
}