namespace LocalDynamoDb.Builder.Docker
{
    internal sealed class DockerConfiguration
    {
        public DockerConfiguration()
        {
            ImageName = LocalDynamoDefault.Docker.DefaultDockerImage;
            ContainerName = LocalDynamoDefault.Docker.DefaultContainerName;
            PortNumber = LocalDynamoDefault.DefaultPortNumber;
            Tag = LocalDynamoDefault.Docker.DefaultTag;
        }

        public string ImageName { get; set; }
        
        public int PortNumber { get; set; }
        
        public string ContainerName { get; set; }
        
        public string Tag { get; set; }
    }
}