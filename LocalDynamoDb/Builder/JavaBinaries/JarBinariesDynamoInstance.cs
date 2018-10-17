using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.Runtime;

namespace LocalDynamoDb.Builder.JavaBinaries
{
    internal class JarBinariesDynamoInstance : IDynamoInstance, IJarBinariesDynamoInstance
    {
        private readonly JarBinariesConfiguration _configuration;

        public JarBinariesDynamoInstance(JarBinariesConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public bool Start()
        {
            throw new System.NotImplementedException();
        }

        public Task Stop()
        {
            throw new System.NotImplementedException();
        }
        
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