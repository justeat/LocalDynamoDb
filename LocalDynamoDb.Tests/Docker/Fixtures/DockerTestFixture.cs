using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace LocalDynamoDb.Tests.Docker.Fixtures
{
    public class DockerClientTestFixture : IDisposable
    {
        private readonly DockerClient _client;

        public DockerClientTestFixture()
        {
            _client = new DockerClientConfiguration(LocalDockerUri()).CreateClient();
        }

        public async Task<IList<ImagesListResponse>> ListImages(string imageName)
            => await _client.Images.ListImagesAsync(new ImagesListParameters { MatchName = imageName });

        public async Task<IList<ImagesListResponse>> ListAllImages()
            => await _client.Images.ListImagesAsync(new ImagesListParameters { All = true });

        public async Task<IList<ContainerListResponse>> Stats()
        {
            return await _client.Containers.ListContainersAsync(new ContainersListParameters(), CancellationToken.None);
        }
        
        private static Uri LocalDockerUri()
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            return isWindows ? new Uri("npipe://./pipe/docker_engine") : new Uri("unix:/var/run/docker.sock");
        }
        
        public void Dispose()
            => _client.Dispose();
    }
}