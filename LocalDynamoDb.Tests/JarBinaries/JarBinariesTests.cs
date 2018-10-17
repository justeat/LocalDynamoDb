using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using LocalDynamoDb.Builder;
using LocalDynamoDb.Tests.JarBinaries.Fixtures;
using Shouldly;
using Xunit;

namespace LocalDynamoDb.Tests.JarBinaries
{
    public class JarBinariesTests : IClassFixture<JarBinariesDynamoFixture>
    {
        private readonly JarBinariesDynamoFixture _fixture;

        public JarBinariesTests(JarBinariesDynamoFixture fixture)
        {
            _fixture = fixture;
        }
  
        [Fact]
        public async Task DynamoDbStarts()
        {
            // Arrange
            await _fixture.Start();
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
            // Arrange
            await _fixture.Start();
            
            // Act
            var state = await _fixture.GetStateAsync();
            
            // Assert
            state.ShouldBe(LocalDynamoDbState.Running);
        }
    }
}