namespace LocalDynamoDb.Builder.Docker
{
    public sealed class DockerConfiguration
    {
        public DockerConfiguration()
        {
            ImageName = LocalDynamoDefault.DefaultDockerImage;
            ContainerName = LocalDynamoDefault.DefaultContainerName;
            PortNumber = LocalDynamoDefault.DefaultPortNumber;
            Tag = LocalDynamoDefault.DefaultTag;
        }

        public string ImageName { get; set; }
        
        public int PortNumber { get; set; }
        
        public string ContainerName { get; set; }
        
        public string Tag { get; set; }
    }
}