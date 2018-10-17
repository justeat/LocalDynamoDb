using Amazon.DynamoDBv2;

namespace LocalDynamoDb.Builder
{
    public interface ICanCreateClient
    {
        AmazonDynamoDBClient CreateClient();
    }
}