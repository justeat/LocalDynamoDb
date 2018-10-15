using System.Threading.Tasks;
using Amazon.DynamoDBv2;

namespace LocalDynamoDb.Builder
{    
    public interface IDockerDynamoInstance
    {
        int PortNumber { get; }
        
        string ContainerName { get; }
        
        string ImageName { get; }
        
        string Tag { get; }
        
        Task<string> GetStateAsync();
    }
    
    public interface IDynamoInstance
    {
        AmazonDynamoDBClient CreateClient();
        
        bool Start();
        
        Task Stop();
    }
}