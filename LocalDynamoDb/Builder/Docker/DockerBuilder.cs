namespace LocalDynamoDb.Builder.Docker
{
    public interface IExposePort
    {
        IDynamoBuilder ExposePort(int portNumber = 8000);
    }
    
    public interface IIsContainer : IExposePort
    {
        IIsContainer UsingDefaultImage();
        
        IIsContainer UsingCustomImage(string imageName);

        IIsContainer ContainerName(string containerName);
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

        public IIsContainer UsingDefaultImage()
            => this;
        

        public IIsContainer UsingCustomImage(string imageName)
        {
            if (!string.IsNullOrWhiteSpace(imageName))
                _configuration.ImageName = imageName;
            
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
            => new DynamoDockerInstance(_configuration);
    }
}