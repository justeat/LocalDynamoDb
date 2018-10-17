using LocalDynamoDb.Builder.Docker;

namespace LocalDynamoDb.Builder.JavaBinaries
{
    internal sealed class JarBinariesConfiguration
    {
        public JarBinariesConfiguration()
        {
            PortNumber = LocalDynamoDefault.DefaultPortNumber;
            Path = LocalDynamoDefault.JarBinaries.DefaultPath;
        }

        public string Path { get; set; }
        
        public int PortNumber { get; set; }
    }
}