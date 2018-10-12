using System.Threading.Tasks;
using Amazon.DynamoDBv2;

namespace LocalDynamoDb.Builder
{    
    public interface IStartStop
    {
        bool Start();
        Task Stop();
    }

    public interface IDynamoInstance : IStartStop
    {
        
        
        AmazonDynamoDBClient Client { get; }   
    }
}