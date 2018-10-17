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
    
    public class JarBinariesBuilder : IJavaBinaries, IDynamoBuilder
    {
        private readonly JarBinariesConfiguration _configuration;

        public JarBinariesBuilder()
        {
            _configuration = new JarBinariesConfiguration();
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
            _configuration.Path = LocalDynamoDefault.JarBinaries.DefaultPath;
            return this;
        }

        public IDynamoBuilder OnPort(int portNumber)
        {
            _configuration.PortNumber = portNumber;
            return this;
        }

        public IDynamoBuilder OnDefaultPort()
        {
            _configuration.PortNumber = LocalDynamoDefault.DefaultPortNumber;
            return this;
        }

        public IDynamoInstance Build()
            => new JarBinariesDynamoInstance(_configuration);
        
    }
}