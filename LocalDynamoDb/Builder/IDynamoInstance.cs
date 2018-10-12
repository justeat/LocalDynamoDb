using System.Threading.Tasks;
using Amazon.DynamoDBv2;

namespace LocalDynamoDb.Builder
{    
    public interface IDynamoInstance
    {
        AmazonDynamoDBClient Client { get; }
        
        bool Start();
        
        Task Stop();
    }
}