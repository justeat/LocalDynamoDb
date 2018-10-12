using LocalDynamoDb.Builder;

namespace LocalDynamoDb.Tests.Fixtures
{
    public class DockerTestFixture
    {
        private readonly IDynamoInstance _instance1;

        public DockerTestFixture()
        {
            var builder = new LocalDynamoDbBuilder().Container().UsingImage("amazon/dynamodb-local").ExposePort(8001);
            _instance1 = builder.Build();
        }

        public void Start()
            => _instance1.Start();
        
        public void Stop()
            => _instance1.Stop();
    }
}