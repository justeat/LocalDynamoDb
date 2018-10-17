namespace LocalDynamoDb.Builder.JavaBinaries
{
    public interface IJarBinariesDynamoInstance
    {
        int PortNumber { get; }
        
        string Path { get; }
    }
}