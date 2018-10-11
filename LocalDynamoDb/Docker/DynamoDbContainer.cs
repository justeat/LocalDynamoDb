using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Docker.DotNet.Models;

namespace LocalDynamoDb.Docker
{
    public class DynamoDbContainer : DockerServer
    {
        private readonly int _portNumber;

        public DynamoDbContainer(string imageName, string containerName, int portNumber) : base(imageName, containerName)
        {
            _portNumber = portNumber;
        }

        protected override async Task<bool> IsReady()
        {
            try
            {
                var config = new AmazonDynamoDBConfig { ServiceURL = $"http://localhost:{_portNumber}"};
                var credentials = new BasicAWSCredentials("A NIGHTINGALE HAS NO NEED FOR KEYS", "IT OPENS DOORS WITH ITS SONG");
                var client = new AmazonDynamoDBClient(credentials, config);
                
                var t = await client.ListTablesAsync();
                return t.HttpStatusCode == HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public override HostConfig ToHostConfig()
        {
            return new HostConfig
            {
                PortBindings = new Dictionary<string, IList<PortBinding>>
                {
                    {
                        $"{_portNumber}/tcp",
                        new List<PortBinding>
                        {
                            new PortBinding
                            {
                                HostPort = $"{_portNumber}",
                                HostIP = "localhost"
                            }
                        }
                    },
                },
            };
        }

        public override Config ToConfig()
        {
            return new Config
            {
                /*Env = new List<string> { "ACCEPT_EULA=Y", "SA_PASSWORD=P@55w0rd", "MSSQL_PID=Developer" }*/
            };
        }
    }
}