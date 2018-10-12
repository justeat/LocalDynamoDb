using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using LocalDynamoDb.Tests.Fixtures;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace LocalDynamoDb.Tests
{
    public class DockerDynamoTests : IClassFixture<DockerTestFixture>
    {
        private readonly DockerTestFixture _fixture;
        private readonly ITestOutputHelper _output;

        public DockerDynamoTests(DockerTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }
                
        [Fact]
        public async Task ShouldStart()
        {
            _output.WriteLine("Creating tables.");
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

            var result = await _fixture.Client.CreateTableAsync(tableRequest);
            result.HttpStatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}