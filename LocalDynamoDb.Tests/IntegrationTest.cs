using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Xunit;
using Xunit.Abstractions;

namespace LocalDynamoDb.Tests
{
    public class IntegrationTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public IntegrationTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
        
        [Fact]
        public async Task FullIntegrationTest()
        {
            var localDynamo = new LocalDynamo(8001);

            try
            {
                localDynamo.Start();
                await Task.Delay(4000);
                await CreateTestTable(localDynamo.Client);

                var tables = await localDynamo.Client.ListTablesAsync();
                Assert.True(tables.TableNames.Count == 1, "Wrong number of tables.  Expected 1 but got " + tables.TableNames.Count);
            }
            finally
            {
                localDynamo.Stop();
            }
        }

        private async Task CreateTestTable(IAmazonDynamoDB dynamoClient)
        {
            _outputHelper.WriteLine("Creating tables.");
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
                _outputHelper.WriteLine("Table already exists.");
                throw;
            }
        }
    }
}