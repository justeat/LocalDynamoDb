using System;
using Amazon.DynamoDBv2;
using LocalDynamoDb.Builder;
using LocalDynamoDb.Tests.Docker.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace LocalDynamoDb.Tests.Docker
{
    public class DockerTests : IClassFixture<DockerClientTestFixture>, IDisposable
    {
        private readonly DockerClientTestFixture _dockerClient;
        private readonly ITestOutputHelper _output;
        private readonly AmazonDynamoDBClient _client;

        public DockerTests(DockerClientTestFixture dockerClient, ITestOutputHelper output)
        {
            _dockerClient = dockerClient;
            _output = output;
            
            var builder = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort();
            var dynamo = builder.Build();
            dynamo.Start();
            
            _client = dynamo.CreateClient();
        }
        
        /*[Fact]*/
        public void PullsContainer()
        {
        }
        
        public void Dispose()
            => _client.Dispose();
    }
}