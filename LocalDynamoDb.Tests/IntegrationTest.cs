using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using LocalDynamoDb.Builder.Docker.Internals;
using Xunit;
using Xunit.Abstractions;

namespace LocalDynamoDb.Tests
{
    public class IntegrationTest
    {
        private readonly ITestOutputHelper _output;

        public IntegrationTest(ITestOutputHelper output)
        {
            _output = output;
        }
        
        [Fact]
        public async Task FullIntegrationTest()
        {
/*
            var localDynamo = new DynamoDbContainer("jar path",8001);

            try
            {
                localDynamo.Start(lo);
                
                await Task.Delay(4000);
                await CreateTestTable(localDynamo.Client);

                var tables = await localDynamo.Client.ListTablesAsync();
                Assert.True(tables.TableNames.Count == 1, "Wrong number of tables. Expected 1 but got " + tables.TableNames.Count);
            }
            finally
            {
                localDynamo.Stop();
            }
*/
        }

        private async Task CreateTestTable(IAmazonDynamoDB dynamoClient)
        {
            _output.WriteLine("Creating tables.");
            var request = new CreateTableRequest
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

            try
            {
                await dynamoClient.CreateTableAsync(request);
            }
            catch (ResourceInUseException)
            {
                _output.WriteLine("Table already exists.");
                throw;
            }
        }
    }
}