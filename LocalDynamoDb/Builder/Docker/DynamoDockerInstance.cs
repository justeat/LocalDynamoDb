using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Docker.DotNet;
using LocalDynamoDb.Builder.Docker.Internals;

namespace LocalDynamoDb.Builder.Docker
{
    public class DynamoDockerInstance : IDynamoInstance
    {
        private readonly DockerConfiguration _configuration;
        private readonly IDockerClient _dockerClient;
        private readonly DynamoDbContainer _container;
        private readonly string _containerName;
        
        public DynamoDockerInstance(DockerConfiguration configuration)
        {
            _containerName = configuration.ContainerNameGenerator();
            _configuration = configuration;
            
            _dockerClient = new DockerClientConfiguration(new Uri("unix:///var/run/docker.sock")).CreateClient();
            _container = new DynamoDbContainer(configuration.ImageName, _containerName, configuration.PortNumber);
        }

        public int GetPortNumber() 
            => _configuration.PortNumber;

        public string GetContainerName()
            => _containerName;

        public string GetImageName()
            => _configuration.ImageName;

        public bool Start()
        {
            try
            {
                // TODO Perhaps return task and let consumer wait?
                _container.Start(_dockerClient).Wait(TimeSpan.FromSeconds(60));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return true;
        }

        public async Task Stop()
        {
            await _container.Stop(_dockerClient);
        }

        public IDynamoInstance Build()
        {
            var config = new AmazonDynamoDBConfig { ServiceURL = $"http://localhost:{_configuration.PortNumber}"};
            var credentials = new BasicAWSCredentials("A NIGHTINGALE HAS NO NEED FOR KEYS", "IT OPENS DOORS WITH ITS SONG");
            
            Client = new AmazonDynamoDBClient(credentials, config);
            return this;
        }

        public AmazonDynamoDBClient Client { get; private set; }
    }
}