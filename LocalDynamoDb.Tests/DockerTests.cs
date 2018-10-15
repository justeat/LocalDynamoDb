using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using LocalDynamoDb.Builder;
using LocalDynamoDb.Builder.Docker;
using LocalDynamoDb.Tests.Fixtures;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace LocalDynamoDb.Tests
{
    public class DockerTests : IClassFixture<DockerClientTestFixture>, IDisposable
    {
        private readonly DockerClientTestFixture _dockerClient;
        private readonly ITestOutputHelper _output;
        private readonly IDynamoInstance _dynamo;
        private readonly AmazonDynamoDBClient _client;

        public DockerTests(DockerClientTestFixture dockerClient, ITestOutputHelper output)
        {
            _dockerClient = dockerClient;
            _output = output;
            
            var builder = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort();
            _dynamo = builder.Build();
            _dynamo.Start();
            
            _client = _dynamo.CreateClient();
        }
        
        [Fact]
        public void PullsContainer()
        {
        }
        
        public void Dispose()
            => _client.Dispose();
    }
}