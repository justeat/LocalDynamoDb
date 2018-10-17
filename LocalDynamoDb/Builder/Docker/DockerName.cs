namespace LocalDynamoDb.Builder.Docker
{
    internal static class LocalDynamoDefault
    {
        internal static class Docker
        {
            public const string DefaultDockerImage = "amazon/dynamodb-local";
            public const string DefaultContainerName = "docker-dynamo-local";
            public const string DefaultTag = "latest";
        }
        
        internal static class JarBinaries
        {
            public const string DefaultPath = "./dynamodblocal/";    
        }

        public const int DefaultPortNumber = 8000;
    }
}