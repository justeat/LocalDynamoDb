using System;
using LocalDynamoDb.Builder;
using LocalDynamoDb.Builder.Docker;
using Shouldly;
using Xunit;

namespace LocalDynamoDb.Tests
{
    public class BuilderTests
    {
        public BuilderTests()
        {
            /*ICanCreateClient builder2 = new LocalDynamoDbBuilder().JarBinaries().InPath("path").OnPort(8001);
            AmazonDynamoDBClient client2 = builder2.CreateClient();*/
        }
        
        [Fact]
        public void CanBuildDockerDynamoInstance()
        {
            var builder1 = new LocalDynamoDbBuilder().Container().UsingImage("amazon/dynamodb-local").ExposePort(8001);
            var instance1 = builder1.Build();
            
            instance1.ShouldNotBeNull();
            instance1.ShouldBeAssignableTo<DynamoDockerInstance>();
        }
        
        [Fact]
        public void ConfigurationShouldBeCorrect()
        {
            var image = "amazon/dynamodb-local";
            var port = 8001;
            var containerName = "docker-dynamo-local";
            
            var builder1 = new LocalDynamoDbBuilder().Container().UsingImage(image).ContainerName(() => containerName).ExposePort(port);
            var instance1 = (DynamoDockerInstance)builder1.Build();
            
            instance1.GetImageName().ShouldBe(image);
            instance1.GetPortNumber().ShouldBe(port);
            instance1.GetContainerName().ShouldBe(containerName);
        }
    }
}