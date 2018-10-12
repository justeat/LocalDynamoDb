using System;
using Amazon.DynamoDBv2;
using LocalDynamoDb.Builder;

namespace LocalDynamoDb.Tests.Fixtures
{
    public class DockerTestFixture : IDisposable
    {
        private readonly IDynamoInstance _dynamo;

        public DockerTestFixture()
        {
            var builder = new LocalDynamoDbBuilder().Container().UsingImage("amazon/dynamodb-local").ExposePort(8000);
            _dynamo = builder.Build();
            _dynamo.Start();
        }

        public void Start()
            => _dynamo.Start();
        
        public void Stop()
            => _dynamo.Stop();

        public void Dispose()
            => _dynamo.Stop();

        public AmazonDynamoDBClient Client
            => _dynamo.CreateClient();
    }
}