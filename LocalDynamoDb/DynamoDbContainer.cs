using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Docker.DotNet.Models;

namespace LocalDynamoDb
{
    public class DynamoDbContainer : DockerServer
    {
        public DynamoDbContainer(string imageName, string containerName) : base(imageName, containerName)
        {
        }

        protected override async Task<bool> IsReady()
        {
            try
            {
                var config = new AmazonDynamoDBConfig { ServiceURL = $"http://localhost:8000"};
                var credentials = new BasicAWSCredentials("A NIGHTINGALE HAS NO NEED FOR KEYS", "IT OPENS DOORS WITH ITS SONG");
                var client = new AmazonDynamoDBClient(credentials, config);
                
                ListTablesResponse t = await client.ListTablesAsync();
                if (t.HttpStatusCode == HttpStatusCode.OK)
                    return true;
                
                return false;
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
                        "8000/tcp",
                        new List<PortBinding>
                        {
                            new PortBinding
                            {
                                HostPort = $"8000",
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