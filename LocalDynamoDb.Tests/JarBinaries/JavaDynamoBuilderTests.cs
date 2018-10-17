using Amazon.DynamoDBv2;
using LocalDynamoDb.Builder;
using LocalDynamoDb.Builder.JavaBinaries;
using Shouldly;
using Xunit;

namespace LocalDynamoDb.Tests.JarBinaries
{
    public class JavaDynamoBuilderTests
    {
        [Fact]
        public void CanBuildJavaDynamoInstance()
        {
            // Arrange
            var builder = new LocalDynamoDbBuilder().JarBinaries().InDefaultPath().OnDefaultPort();
            
            // Act
            var dynamo = builder.Build();

            // Assert
            dynamo.ShouldBeAssignableTo<IJarBinariesDynamoInstance>();
        }
        
        [Fact]
        public void DefaultPathIsSet()
        {
            // Arrange
            var builder = new LocalDynamoDbBuilder().JarBinaries().InDefaultPath().OnDefaultPort();
            
            // Act
            var dynamo = (IJarBinariesDynamoInstance) builder.Build();

            // Assert
            dynamo.Path.ShouldBe("./dynamodblocal/");
        }
        
        [Fact]
        public void DefaultPortIsSet()
        {
            // Arrange
            var builder = new LocalDynamoDbBuilder().JarBinaries().InDefaultPath().OnDefaultPort();
            
            // Act
            var dynamo = (IJarBinariesDynamoInstance) builder.Build();

            // Assert
            dynamo.PortNumber.ShouldBe(8000);
        }
        
        [Fact]
        public void CustomPortIsSet()
        {
            // Arrange
            var builder = new LocalDynamoDbBuilder().JarBinaries().InDefaultPath().OnPort(8001);
            
            // Act
            var dynamo = (IJarBinariesDynamoInstance) builder.Build();

            // Assert
            dynamo.PortNumber.ShouldBe(8001);
        }
        
        [Fact]
        public void AmazonDynamoDbClientIsCreated()
        {
            // Arrange
            var builder = new LocalDynamoDbBuilder().JarBinaries().InDefaultPath().OnDefaultPort();
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
            var builder = new LocalDynamoDbBuilder().JarBinaries().InDefaultPath().OnPort(8001);
            var dynamo = builder.Build();
            
            // Act
            var client = dynamo.CreateClient();

            // Assert
            client.Config.ServiceURL.ShouldBe("http://localhost:8001");
        }
    }
}