using System;

namespace LocalDynamoDb.Builder.Docker
{
    public class DockerConfiguration
    {
        public DockerConfiguration()
        {
            ImageName = "amazon/dynamodb-local";
            ContainerNameGenerator = () => "dynamodb-local" + Guid.NewGuid();
            PortNumber = 8000;
        }

        public string ImageName { get; set; }
        public int PortNumber { get; set; }
        public Func<string> ContainerNameGenerator { get; set; }
    }
}