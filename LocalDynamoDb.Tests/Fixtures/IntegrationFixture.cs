using System;
using Docker.DotNet;
using LocalDynamoDb.Builder.Docker.Internals;

namespace LocalDynamoDb.Tests
{
    public class IntegrationFixture
    {
        private readonly IDockerClient _client;
        private readonly DynamoDbContainer _container;
        
        public IntegrationFixture()
        {
            _client = new DockerClientConfiguration(new Uri("unix:///var/run/docker.sock")).CreateClient();
            _container = new DynamoDbContainer("amazon/dynamodb-local", "je-dynamotest", 8001);
        }

        public bool Start()
        {
            try
            {
                _container.Start(_client).Wait(TimeSpan.FromSeconds(60));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return true;
        }
    }}