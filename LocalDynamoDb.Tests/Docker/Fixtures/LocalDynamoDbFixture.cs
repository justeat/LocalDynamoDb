using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using LocalDynamoDb.Builder;
using LocalDynamoDb.Builder.Docker;

namespace LocalDynamoDb.Tests.Docker.Fixtures
{
    public class LocalDynamoFixture : IDisposable
    {
        private readonly IDynamoInstance _dynamo;
        private AmazonDynamoDBClient _client;

        public LocalDynamoFixture()
        {
            var builder = new LocalDynamoDbBuilder().Container().UsingDefaultImage().ExposePort();
            _dynamo = builder.Build();

            _dynamo.Start();
        }

        public AmazonDynamoDBClient Client
            => _client ?? (_client = _dynamo.CreateClient());

        public Task<string> GetStateAsync()
        {
            var d = (IDockerDynamoInstance) _dynamo;
            return d.GetStateAsync();
        }

        public void Dispose()
        {
            _dynamo.Stop();
            _client?.Dispose();
        }
    }
}