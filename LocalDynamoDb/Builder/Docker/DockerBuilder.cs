using System;

namespace LocalDynamoDb.Builder.Docker
{
    public interface IExposePort
    {
        IDynamoBuilder ExposePort(int portNumber);
    }
    
    public interface IIsContainer : IExposePort
    {
        IIsContainer UsingImage(string imageName);

        IIsContainer ContainerName(Func<string> containerName);
    }
    
    public interface IDynamoBuilder
    {
        IDynamoInstance Build();
    }
    
    public class DockerBuilder : IIsContainer, IDynamoBuilder
    {
        private readonly DockerConfiguration _configuration;

        public DockerBuilder()
        {
            _configuration = new DockerConfiguration();
        }
        
        public IIsContainer UsingImage(string imageName)
        {
            if (!string.IsNullOrWhiteSpace(imageName))
                _configuration.ImageName = imageName;
            
            return this;
        }
    
        public IIsContainer ContainerName(Func<string> containerName)
        {
            if (containerName != null)
                _configuration.ContainerNameGenerator = containerName;
            
            return this;
        }

        public IDynamoBuilder ExposePort(int portNumber)
        {
            _configuration.PortNumber = portNumber;
            return this;
        }

        public IDynamoInstance Build()
            => new DynamoDockerInstance(_configuration);
    }
}