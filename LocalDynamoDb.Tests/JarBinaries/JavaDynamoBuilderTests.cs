using LocalDynamoDb.Builder;
using LocalDynamoDb.Builder.JavaBinaries;
using Shouldly;
using Xunit;

namespace LocalDynamoDb.Tests.JarBinaries
{
    public class JavaDynamoBuilderTests
    {
        [Fact]
        public void CanBuildJavaDynamoInstance()
        {
            var builder1 = new LocalDynamoDbBuilder().JarBinaries().InDefaultPath().OnDefaultPort();
            var instance1 = builder1.Build();

            instance1.ShouldNotBeNull();
            instance1.ShouldBeAssignableTo<IJavaBinaries>();
        }
    }
}