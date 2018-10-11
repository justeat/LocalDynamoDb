using System;
using System.Linq;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace LocalDynamoDb.Docker
{
    public abstract class DockerServer
    {
        public string ImageName { get; }
        public string ContainerName { get; }

        protected DockerServer(string imageName, string containerName)
        {
            ImageName = imageName;
            ContainerName = containerName + "-" + Guid.NewGuid().ToString();
        }

        public async Task Start(IDockerClient client)
        {
            if (StartAction != StartAction.None)
                return;
            
            var images = await client.Images.ListImagesAsync(new ImagesListParameters { MatchName = ImageName });
            if (images.Count == 0)
            {
                Console.WriteLine($"Fetching Docker image '{ImageName}'");

                /*await client.Images.CreateImageAsync(new ImagesCreateParameters { FromImage = ImageName, Tag = "latest" });
                
                Stream stream  = await client.Images.CreateImageAsync(new ImagesCreateParameters() {
                    Parent = "fedora/memcached",
                    Pa
                    Tag = "alpha",
                }, new AuthConfig(){
                    Email = "ahmetb@microsoft.com",
                    Username = "ahmetalpbalkan",
                    Password = "pa$$w0rd"
                });*/
            }

            var list = await client.Containers.ListContainersAsync(new ContainersListParameters
            {
                All = true
            });

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

            var started = await client.Containers.StartContainerAsync(ContainerName, new ContainerStartParameters());
            if (!started)
            {
                throw new InvalidOperationException($"Container '{ContainerName}' did not start!!!!");
            }

            var i = 0;
            
            while (true)
            {
                var r = await IsReady();
                if (r)
                    return;
                
                i++;

                if (i > 20)
                {
                    throw new TimeoutException($"Container {ContainerName} does not seem to be responding in a timely manner");
                }

                await Task.Delay(TimeSpan.FromSeconds(5));
            }

            Console.WriteLine($"Container '{ContainerName}' is ready.");

            StartAction = StartAction.Started;
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
        {
            await client.Containers.StopContainerAsync(ContainerName, new ContainerStopParameters());
        }

        public Task Remove(IDockerClient client)
        {
            return client.Containers.RemoveContainerAsync(ContainerName,
                new ContainerRemoveParameters { Force = true });
        }

        protected abstract Task<bool> IsReady();

        public abstract HostConfig ToHostConfig();

        public abstract Config ToConfig();

        public override string ToString()
        {
            return $"{nameof(ImageName)}: {ImageName}, {nameof(ContainerName)}: {ContainerName}";
        }
    }

    public enum StartAction
    {
        None,
        Started,
        External
    }
}