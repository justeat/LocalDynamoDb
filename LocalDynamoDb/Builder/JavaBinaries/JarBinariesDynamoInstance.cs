using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using LocalDynamoDb.Builder.JavaBinaries.Internals;

namespace LocalDynamoDb.Builder.JavaBinaries
{
    internal class JarBinariesDynamoInstance : IDynamoInstance, IJarBinariesDynamoInstance
    {
        private readonly JarBinariesConfiguration _configuration;
        private readonly DynamoProcessHandler _process;
        
        public JarBinariesDynamoInstance(JarBinariesConfiguration configuration)
        {
            _configuration = configuration;
            _process = new DynamoProcessHandler(_configuration);
        }
        
        public Task<bool> Start()
            => _process.Start();

        public Task Stop()
            => _process.Stop();

        public Task<LocalDynamoDbState> GetStateAsync()
            => Task.FromResult(_process.IsResponding() ? LocalDynamoDbState.Running : LocalDynamoDbState.Stopped);

        public AmazonDynamoDBClient CreateClient()
        {
            var config = new AmazonDynamoDBConfig { ServiceURL = $"http://localhost:{_configuration.PortNumber}"};
            var credentials = new BasicAWSCredentials("A NIGHTINGALE HAS NO NEED FOR KEYS", "IT OPENS DOORS WITH ITS SONG");
            return new AmazonDynamoDBClient(credentials, config);
        }

        public int PortNumber
            => _configuration.PortNumber;

        public string Path
            => _configuration.Path;
    }
}