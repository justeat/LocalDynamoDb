using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Xunit;

namespace LocalDynamoDb.Tests
{
    public static class IntegrationTest
    {
        [Fact]
        public static async Task FullIntegrationTest()
        {
            var localDynamo = new LocalDynamo(8001);

            try
            {
                localDynamo.Start();
                await CreateTestTable(localDynamo.Client);

                var tables = await localDynamo.Client.ListTablesAsync();
                Assert.True(tables.TableNames.Count == 1, "Wrong number of tables.  Expected 1 but got " + tables.TableNames.Count);
            }
            finally
            {
                localDynamo.Stop();
            }
        }

        private static async Task CreateTestTable(IAmazonDynamoDB dynamoClient)
        {
            Console.WriteLine("Creating tables.");
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
                Console.WriteLine("Table already exists.");
                throw;
            }
        }
    }
}