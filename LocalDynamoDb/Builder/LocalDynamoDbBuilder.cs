using LocalDynamoDb.Builder.Docker;
using LocalDynamoDb.Builder.JavaBinaries;

namespace LocalDynamoDb.Builder
{
    public interface ILocalDynamoDbBuilder
    {
        DockerBuilder Container();

        JarBinariesBuilder JarBinaries();
    }
    
    public class LocalDynamoDbBuilder
    {
        public IJavaBinaries JarBinaries()
            => new JarBinariesBuilder();

        public IIsContainer Container()
            => new DockerBuilder();
    }
}