using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using LocalDynamoDb.Builder;

namespace LocalDynamoDb.Tests.JarBinaries.Fixtures
{
    public class JarBinariesDynamoFixture : IDisposable
    {
        private readonly IDynamoInstance _dynamo;
        private AmazonDynamoDBClient _client;

        public JarBinariesDynamoFixture()
        {
            var builder = new LocalDynamoDbBuilder().JarBinaries().InDefaultPath().OnDefaultPort();
            _dynamo = builder.Build();
        }

        public Task<bool> Start()
            => _dynamo.Start();
        
        public AmazonDynamoDBClient Client
            => _client ?? (_client = _dynamo.CreateClient());

        public Task<LocalDynamoDbState> GetStateAsync()
            => _dynamo.GetStateAsync();

        public void Dispose()
        {
            _dynamo.Stop();
            _client?.Dispose();
        }
    }
}