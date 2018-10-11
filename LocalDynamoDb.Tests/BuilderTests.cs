using LocalDynamoDb.Builder;

namespace LocalDynamoDb.Tests
{
    public class BuilderTests
    {
        public BuilderTests()
        {
            var builder1 = new LocalDynamoDbBuilder().Container().UsingImage("image name").CreateClient();
            var builder2 = new LocalDynamoDbBuilder().JarBinaries().AtPath("path").CreateClient();
            
            builder1.Cre
        }
    }
}