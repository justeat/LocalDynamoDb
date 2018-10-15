using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Docker.DotNet;
using Docker.DotNet.Models;
using static System.Threading.CancellationToken;

namespace LocalDynamoDb.Builder.Docker.Internals
{
    internal abstract class DockerServer
    {
        public string ImageName { get; }
        public string ContainerName { get; }

        protected DockerServer(string imageName, string containerName)
        {
            ImageName = imageName;
            ContainerName = containerName;
        }

        public async Task<string> GetStateAsync(IDockerClient client)
        {
            var list = await client.Containers.ListContainersAsync(new ContainersListParameters
            {
                All = true
            });
            
            var container = list.FirstOrDefault(x => x.Names.Contains("/" + ContainerName));
            return container.State;
        }

        public async Task Start(IDockerClient client)
        {
            if (StartAction != StartAction.None)
                return;
            
            var images = await client.Images.ListImagesAsync(new ImagesListParameters { MatchName = ImageName });
            if (images.Count == 0)
            {
                Console.WriteLine($"Fetching Docker image '{ImageName}'");
                var progress = new Progress<JSONMessage>(x =>
                {
                    Console.WriteLine("Status: " + x.Status);
                });
                
                var cts = new CancellationTokenSource();
                await client.Images.CreateImageAsync(new ImagesCreateParameters { FromImage = ImageName, Tag = "latest" }, new AuthConfig(), progress, cts.Token);
            }

            var list = await client.Containers.ListContainersAsync(new ContainersListParameters
            {
                All = true
            });

            if (!string.IsNullOrWhiteSpace(ContainerName))
            {
                var container = list.FirstOrDefault(x => x.Names.Contains("/" + ContainerName));
                if (container == null)
                {
                    await CreateContainer(client);
                }
                else
                {
                    if (container.State == "running")
                    {
                        Console.WriteLine($"Container '{ContainerName}' is already running.");
                        StartAction = StartAction.External;
                        return;
                    }
                }    
            }

            var started = await client.Containers.StartContainerAsync(ContainerName, new ContainerStartParameters());
            if (!started)
            {
                throw new InvalidOperationException($"Container '{ContainerName}' did not start!");
            }

            var i = 0;
            while (true)
            {
                var r = await IsReady();
                if (r)
                {
                    Console.WriteLine($"Container '{ContainerName}' is ready.");
                    StartAction = StartAction.Started;
                    return;
                }
                
                i++;

                if (i > 20)
                {
                    throw new TimeoutException($"Container {ContainerName} does not seem to be responding in a timely manner");
                }

                await Task.Delay(TimeSpan.FromSeconds(2));
            }   
        }

        public static StartAction StartAction { get; private set; } = StartAction.None;

        private async Task CreateContainer(IDockerClient client)
        {
            Console.WriteLine($"Creating container '{ContainerName}' using image '{ImageName}'");

            var hostConfig = ToHostConfig();
            var config = ToConfig();

            await client.Containers.CreateContainerAsync(new CreateContainerParameters(config)
            {
                Image = ImageName,
                Name = ContainerName,
                Tty = true,
                HostConfig = hostConfig,
            });
        }

        public async Task Stop(IDockerClient client)
            => await client.Containers.StopContainerAsync(ContainerName, new ContainerStopParameters());

        protected abstract Task<bool> IsReady();

        public abstract HostConfig ToHostConfig();

        public abstract Config ToConfig();

        public override string ToString()
            => $"{nameof(ImageName)}: {ImageName}, {nameof(ContainerName)}: {ContainerName}";
    }
}