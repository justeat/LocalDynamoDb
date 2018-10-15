using LocalDynamoDb.Builder;
using LocalDynamoDb.Builder.Docker;
using Shouldly;
using Xunit;

namespace LocalDynamoDb.Tests
{
    public class DockerDynamoBuilderTests
    {
        [Fact]
        public void CanBuildDockerDynamoInstance()
        {
            var builder1 = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort();
            var instance1 = builder1.Build();

            instance1.ShouldNotBeNull();
            instance1.ShouldBeAssignableTo<IDockerDynamoInstance>();
        }

        [Fact]
        public void DefaultImageNameIsSet()
        {
            var builder1 = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort();
            var instance1 = (IDockerDynamoInstance) builder1.Build();

            instance1.ImageName.ShouldBe("amazon/dynamodb-local");
        }

        [Fact]
        public void CustomImageNameIsSet()
        {
            var image = "my-custom-image";

            var builder1 = new LocalDynamoDbBuilder().Container().UsingCustomImage(image).ExposePort();
            var instance1 = (IDockerDynamoInstance) builder1.Build();

            instance1.ImageName.ShouldBe(image);
        }

        [Fact]
        public void DefaultPortIsSet()
        {
            var builder1 = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort();
            var instance1 = (IDockerDynamoInstance) builder1.Build();

            instance1.PortNumber.ShouldBe(8000);
        }

        [Fact]
        public void CustomPortIsSet()
        {
            var customPort = 8001;

            var builder1 = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort(customPort);
            var instance1 = (IDockerDynamoInstance) builder1.Build();

            instance1.PortNumber.ShouldBe(customPort);
        }

        [Fact]
        public void DefaultContainerNameIsSet()
        {
            var builder1 = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort();
            var instance1 = (IDockerDynamoInstance) builder1.Build();

            instance1.ContainerName.ShouldBe("docker-dynamo-local");
        }


        [Fact]
        public void CustomContainerNameIsSet()
        {
            var builder1 = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ContainerName("custom-name")
                .ExposePort();
            var instance1 = (IDockerDynamoInstance) builder1.Build();

            instance1.ContainerName.ShouldBe("custom-name");
        }

        [Fact]
        public void DefaultTagIsLatest()
        {
            var builder1 = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort();
            var instance1 = (IDockerDynamoInstance) builder1.Build();

            instance1.Tag.ShouldBe("latest");
        }

        [Fact]
        public void CustomTagIsSet()
        {
            var tag = "1.11.119";

            var builder1 = new LocalDynamoDbBuilder().Container().UsingDefaultImage(tag).ExposePort();
            var instance1 = (IDockerDynamoInstance) builder1.Build();

            instance1.Tag.ShouldBe(tag);
        }
    }
}