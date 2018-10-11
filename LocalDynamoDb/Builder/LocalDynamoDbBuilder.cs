using Amazon.DynamoDBv2;

namespace LocalDynamoDb.Builder
{
    public interface ILocalDynamoDbBuilder
    {
        IHasContainer Container();
        IHasPath JarBinaries();
    }

    public interface IHasContainer
    {
        ICreateClient UsingImage(string imageName);
    }
    
    public interface IHasPath
    {
        ICreateClient AtPath(string path);
    }
    
    public interface IHasJarBinaries
    {
        AmazonDynamoDBClient CreateClient();
    }

    public interface ICreateClient
    {
        AmazonDynamoDBClient CreateClient();
    }
    
    public class LocalDynamoDbBuilder : ILocalDynamoDbBuilder, IHasContainer, IHasJarBinaries, ICreateClient, IHasPath
    {
        public IHasPath JarBinaries()
        {
            return this;
        }

        public IHasContainer Container()
        {
            return this;
        }

        public ICreateClient UsingImage(string imageName)
        {
            return this;
        }

        AmazonDynamoDBClient IHasJarBinaries.CreateClient()
        {
            throw new System.NotImplementedException();
        }

        AmazonDynamoDBClient ICreateClient.CreateClient()
        {
            throw new System.NotImplementedException();
        }

        public ICreateClient AtPath(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}