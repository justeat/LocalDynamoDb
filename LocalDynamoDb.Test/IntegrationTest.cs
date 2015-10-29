using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2;
using NUnit.Framework;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;


namespace LocalDynamoDb.Test
{
    [TestFixture]
    public class IntegrationTest
    {
        [Test]
        public void FullIntegrationTest()
        {
            var config = new AmazonDynamoDBConfig { ServiceURL = "http://localhost:8001" };
            var credentials = new BasicAWSCredentials("A NIGHTINGALE HAS NO NEED FOR KEYS", "IT OPENS DOORS WITH ITS SONG");
            var client = new AmazonDynamoDBClient(credentials, config);

            var localDynamo = new LocalDynamo(8001);

            try
            {
                localDynamo.Start();
                CreateTestTable(client);

                var tables = client.ListTables();
                Assert.AreEqual(tables.TableNames.Count, 1,
                    "Wrong number of tables.  Expected 1 but got " + tables.TableNames.Count);
            }
            finally
            {
                localDynamo.Stop();
            }
        }

        public void CreateTestTable(AmazonDynamoDBClient dynamoClient)
        {
            Console.WriteLine("Creating tables.");
            var request = new CreateTableRequest
            {
                TableName = "testTable",
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition
                    {
                        AttributeName = "Id",
                        AttributeType = "N"
                    }
                },
                KeySchema = new List<KeySchemaElement>()
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
                dynamoClient.CreateTable(request);
            }
            catch (ResourceInUseException ex)
            {
                Console.WriteLine("Table already exists.");
                throw ex;
            }
        }
    }
}
