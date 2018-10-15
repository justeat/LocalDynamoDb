using System.Threading.Tasks;
using Amazon.DynamoDBv2;

namespace LocalDynamoDb.Builder.JavaBinaries
{
    public interface IJarBinariesDynamoInstance
    {
        
    }
    
    internal class JarBinariesDynamoInstance : IDynamoInstance, IJarBinariesDynamoInstance
    {
        public AmazonDynamoDBClient CreateClient()
        {
            throw new System.NotImplementedException();
        }

        public bool Start()
        {
            throw new System.NotImplementedException();
        }

        public Task Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}