using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using LocalDynamoDb.Builder.Docker;
using LocalDynamoDb.Builder.JavaBinaries.Internals;

namespace LocalDynamoDb.Builder.JavaBinaries
{
    public interface IJavaBinaries : IUsingPort
    {
        IJavaBinaries InPath(string path);
        
        IJavaBinaries InDefaultPath();
    }
    
    public interface IUsingPort
    {
        IDynamoBuilder OnPort(int portNumber);
        
        IDynamoBuilder OnDefaultPort();
    }
    
    public class JarBuilder : IJavaBinaries, IDynamoBuilder
    {
        private readonly JarBinariesConfiguration _configuration;

        public JarBuilder()
        {
            _configuration = new JarBinariesConfiguration();
        }
        
        public AmazonDynamoDBClient CreateClient()
        {
            return new AmazonDynamoDBClient();
        }

        public IJavaBinaries InPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));
            
            _configuration.Path = path;
            return this;
        }

        public IJavaBinaries InDefaultPath()
        {
            return this;
        }

        public IDynamoBuilder OnPort(int portNumber)
        {
            _configuration.PortNumber = portNumber;

            return this;
        }

        public IDynamoBuilder OnDefaultPort()
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

        public IDynamoInstance Build()
            => new LocalDynamoJar(_configuration);
        
    }
}