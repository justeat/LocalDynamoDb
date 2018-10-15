namespace LocalDynamoDb.Builder.Docker
{
    public interface IExposePort
    {
        IDynamoBuilder ExposePort(int portNumber = 8000);
    }
    
    public interface IIsContainer : IExposePort
    {
        IIsContainer UsingDefaultImage(string tag = "latest");
        
        IIsContainer UsingCustomImage(string imageName, string tag = "latest");

        IIsContainer ContainerName(string containerName);
    }
    
    public class DockerBuilder : IIsContainer, IDynamoBuilder
    {
        private readonly DockerConfiguration _configuration;

        public DockerBuilder()
        {
            _configuration = new DockerConfiguration();
        }

        public IIsContainer UsingDefaultImage(string tag = "latest")
        {
            if (!string.IsNullOrWhiteSpace(tag))
                _configuration.Tag = tag;
            
            return this;
        }
        
        public IIsContainer UsingCustomImage(string imageName, string tag = "latest")
        {
            if (!string.IsNullOrWhiteSpace(imageName))
                _configuration.ImageName = imageName;
            
            if (!string.IsNullOrWhiteSpace(tag))
                _configuration.Tag = tag;
            
            return this;
        }
    
        public IIsContainer ContainerName(string containerName)
        {
            if (!string.IsNullOrWhiteSpace(containerName))
                _configuration.ContainerName = containerName;

            return this;
        }

        public IDynamoBuilder ExposePort(int portNumber)
        {
            _configuration.PortNumber = portNumber;

            return this;
        }

        public IDynamoInstance Build()
            => new DynamoDockerDynamoInstance(_configuration);
    }
}