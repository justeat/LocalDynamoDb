namespace LocalDynamoDb.Builder.Docker
{
    internal static class LocalDynamoDefault
    {
        public const string DefaultDockerImage = "amazon/dynamodb-local";
        public const string DefaultContainerName = "docker-dynamo-local";
        public const string DefaultTag = "latest";
        public const int DefaultPortNumber = 8000;
    }
}