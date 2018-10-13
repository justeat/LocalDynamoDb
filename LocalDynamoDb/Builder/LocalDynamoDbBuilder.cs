using LocalDynamoDb.Builder.Docker;
using LocalDynamoDb.Builder.JavaBinaries;

namespace LocalDynamoDb.Builder
{
    public interface ILocalDynamoDbBuilder
    {
        DockerBuilder Container();

        JarBuilder JarBinaries();
    }
    
    public class LocalDynamoDbBuilder
    {
        public IHasPath JarBinaries()
            => new JarBuilder();

        public IIsContainer Container()
            => new DockerBuilder();
    }
}