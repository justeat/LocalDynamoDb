using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using LocalDynamoDb.Builder;
using LocalDynamoDb.Tests.Docker.Fixtures;
using Shouldly;
using Xunit;

namespace LocalDynamoDb.Tests.Docker
{
    public class DockerDynamoTests : IClassFixture<DockerDynamoFixture>
    {
        private readonly DockerDynamoFixture _fixture;

        public DockerDynamoTests(DockerDynamoFixture fixture)
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
            state.ShouldBe(LocalDynamoDbState.Running);
        }
    }
}