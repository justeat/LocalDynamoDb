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
            var localDynamo = new LocalDynamo(8001);

            try
            {
                localDynamo.Start();
                CreateTestTable(localDynamo.Client);

                var tables = localDynamo.Client.ListTables();
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

    [TestFixture]
    public class ClientGetsRenewedOnStartTest
    {
        private readonly LocalDynamo _localDynamo = new LocalDynamo(8001);

        [Test]
        public void FirstTest()
        {
            RunSimpleTest();
        }

        [Test]
        public void SecondTest()
        {
            RunSimpleTest();
        }

        private void RunSimpleTest()
        {
            try
            {
                _localDynamo.Start();
                CreateTestTable(_localDynamo.Client);

                var tables = _localDynamo.Client.ListTables();
                Assert.AreEqual(tables.TableNames.Count, 1,
                    "Wrong number of tables.  Expected 1 but got " + tables.TableNames.Count);
            }
            finally
            {
                _localDynamo.Stop();
                _localDynamo.Client.Dispose();
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
