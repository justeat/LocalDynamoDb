using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using LocalDynamoDb.Docker;
using static System.FormattableString; 

namespace LocalDynamoDb
{
    public interface ILocalDynamoDb
    {
        void Start();
        void Stop();
    }
    
    public class LocalDynamo : ILocalDynamoDb
    {
        private readonly int _port;
        private DynamoDbContainer _dynamo;
        private Process Dynamo { get; set; }
        public AmazonDynamoDBClient Client { get; private set; }

        public LocalDynamo(string imageName, string containerName, int portNumber = 8000)
        {
            _port = portNumber;
            _dynamo = new DynamoDbContainer(imageName, containerName, portNumber);
        }
        
        public LocalDynamo(string path, int portNumber = 8000)
        {
            _port = portNumber;
        }
        

        public void Start()
        {
            _dynamo.Start();
        }

        public void Stop()
        {
        }

        private AmazonDynamoDBClient CreateClient()
        {
            var config = new AmazonDynamoDBConfig { ServiceURL = Invariant($"http://localhost:{_port}")};
            var credentials = new BasicAWSCredentials("A NIGHTINGALE HAS NO NEED FOR KEYS", "IT OPENS DOORS WITH ITS SONG");
            return new AmazonDynamoDBClient(credentials, config);
        }
    }
}