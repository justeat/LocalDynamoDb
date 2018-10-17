using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.Runtime;

namespace LocalDynamoDb.Builder.JavaBinaries.Internals
{
    internal sealed class DynamoProcessHandler
    {
        private readonly Process _dynamoProcess;

        public DynamoProcessHandler(JarBinariesConfiguration configuration)
        {
            _dynamoProcess = CreateProcess(configuration.PortNumber, configuration.Path);
        }

        private static Process CreateProcess(int portNumber, string path)
        {
            var processJar = new Process();
            var arguments = $"-Djava.library.path=.{Path.DirectorySeparatorChar}DynamoDBLocal_lib -jar DynamoDBLocal.jar curl -O https://bootstrap.pypa.io/get-pip.py -port {portNumber}";

            processJar.StartInfo.FileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\"" + @"java" + "\"" : "java";;
            processJar.StartInfo.Arguments = arguments;

            var rootFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            /*var relativePath = Path.DirectorySeparatorChar + "dynamodblocal";*/
            var relativePath = Path.DirectorySeparatorChar + "dynamodblocal";
            var osFriendlyAbsPath = Path.GetFullPath(rootFolder + relativePath).Replace('\\', Path.DirectorySeparatorChar);
            var jarFilePath = Path.Combine(osFriendlyAbsPath, "DynamoDBLocal.jar");

            Console.WriteLine("Jar file path - " + jarFilePath);

            if (!File.Exists(jarFilePath))
            {
                throw new FileNotFoundException(
                    "DynamoDBLocal.jar not found in " + osFriendlyAbsPath +
                    ". Please review the README.txt for setup instructions.",
                    jarFilePath);
            }

            processJar.StartInfo.WorkingDirectory = osFriendlyAbsPath;
            processJar.StartInfo.UseShellExecute = false;
            processJar.StartInfo.RedirectStandardOutput = true;
            processJar.StartInfo.RedirectStandardError = true;
            
            return processJar;
        }

        public Task<bool> Start()
        {
            Console.WriteLine("Starting in memory DynamoDb");
            var success = _dynamoProcess.Start();
            
            if (!success)
                throw new Exception("Error starting dynamo: " + _dynamoProcess.StandardError.ReadToEnd());

            return Task.FromResult(true);
        }

        public bool IsResponding()
            => _dynamoProcess.Responding;

        public Task Stop()
        {
            Console.WriteLine("Stopping in memory DynamoDb");
            try
            {
                _dynamoProcess.Kill();
            }
            catch (Win32Exception)
            {
                Console.WriteLine(_dynamoProcess.StandardError.ReadToEnd());
                throw;
            }

            return Task.CompletedTask;
        }
    }

}