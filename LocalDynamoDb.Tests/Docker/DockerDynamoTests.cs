using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using LocalDynamoDb.Tests.Docker.Fixtures;
using Shouldly;
using Xunit;

namespace LocalDynamoDb.Tests.Docker
{
    public class DockerDynamoTests : IClassFixture<LocalDynamoFixture>
    {
        private readonly LocalDynamoFixture _fixture;

        public DockerDynamoTests(LocalDynamoFixture fixture)
        {
            _fixture = fixture;
        }
  
        [Fact]
        public async Task DynamoDbStarts()
        {
            // Arrange
            var tableRequest = new CreateTableRequest
            {
                TableName = "testTable",
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName = "Id",
                        AttributeType = "N"
                    }
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "Id",
                        KeyType = "HASH"
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 1,
                    WriteCapacityUnits = 1
                }
            };

            // Act
            var result = await _fixture.Client.CreateTableAsync(tableRequest);
            
            // Assert
            result.HttpStatusCode.ShouldBe(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task StateIsRunning()
        {
            // Act
            var state = await _fixture.GetStateAsync();
            
            // Assert
            state.ShouldBe("running");
        }
    }
}