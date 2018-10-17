namespace LocalDynamoDb.Builder.Docker
{
    public interface IDockerDynamoInstance
    {
        int PortNumber { get; }
        
        string ContainerName { get; }
        
        string ImageName { get; }
        
        string Tag { get; }
    }
}