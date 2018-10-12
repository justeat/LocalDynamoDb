using System.Threading.Tasks;
using Amazon.DynamoDBv2;

namespace LocalDynamoDb.Builder
{    
    public interface IDynamoInstance
    {
        /*AmazonDynamoDBClient Client { get; }*/

        AmazonDynamoDBClient CreateClient();
        
        bool Start();
        
        Task Stop();
    }
}