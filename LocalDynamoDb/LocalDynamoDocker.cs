using System;
using Docker.DotNet;
using LocalDynamoDb.Docker;

namespace LocalDynamoDb
{
    public class LocalDynamoDocker : ILocalDynamoDb
    {
        private DockerClient _dockerClient;
        private DynamoDbContainer _container;

        public LocalDynamoDocker(string imageName, string containerName, int portNumber)
        {
            // TODO: Base URI on OS
            _dockerClient = new DockerClientConfiguration(new Uri("unix:///var/run/docker.sock")).CreateClient();
            _container = new DynamoDbContainer(imageName, containerName, 8000);
        }

        public override void Start()
        {
            throw new System.NotImplementedException();
        }

        public override void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}