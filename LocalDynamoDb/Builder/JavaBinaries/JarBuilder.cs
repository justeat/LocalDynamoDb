using System.Threading.Tasks;
using Amazon.DynamoDBv2;

namespace LocalDynamoDb.Builder.JavaBinaries
{
    public interface IHasPath
    {
        IUsingPort InPath(string path);
    }
    
    public interface IUsingPort
    {
        ICanCreateClient OnPort(int portNumber);
    }
    
    public class JarBuilder : ICanCreateClient, IHasPath, IUsingPort
    {
        public AmazonDynamoDBClient CreateClient()
        {
            return new AmazonDynamoDBClient();
        }

        public IUsingPort InPath(string path)
        {
            return this;
        }

        public ICanCreateClient OnPort(int portNumber)
        {
            return this;
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