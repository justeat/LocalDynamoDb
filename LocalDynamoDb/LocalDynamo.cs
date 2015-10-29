using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace LocalDynamoDb
{
    public class LocalDynamo
    {
        private Process Dynamo { get; set; }

        public LocalDynamo(int portNumber = 8000)
        {
            Dynamo = Create(portNumber);
        }

        private Process Create(int portNumber)
        {
            var processJar = new Process();
            var arguments = String.Format("-Djava.library.path=./DynamoDBLocal_lib -jar DynamoDBLocal.jar -sharedDb -inMemory -port{0}", portNumber);

            processJar.StartInfo.FileName = "\"" + @"java" + "\"";
            processJar.StartInfo.Arguments = arguments;

            var rootFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var relativePath = "\\dynamodblocal";
            string absolutePath = Path.GetFullPath(rootFolder + relativePath);
            var jarFilePath = absolutePath + "\\DynamoDBLocal.jar";

            if (!File.Exists(jarFilePath))
            {
                throw new FileNotFoundException(
                    "DynamoDBLocal.jar not found in " + absolutePath +
                    ". Please review the README.txt for setup instructions.",
                    jarFilePath);
            }

            processJar.StartInfo.WorkingDirectory = absolutePath;
            processJar.StartInfo.UseShellExecute = false;
            processJar.StartInfo.RedirectStandardOutput = true;
            processJar.StartInfo.RedirectStandardError = true;
            return processJar;
        }

        public void Start()
        {
            Console.WriteLine("Starting in memory DynamoDb");
            var success = Dynamo.Start();
            if (!success)
            {
                throw new Win32Exception("Error starting dynamo: " + Dynamo.StandardError.ReadToEnd());
            }
        }

        public void Stop()
        {
            Console.WriteLine("Stopping in memory DynamoDb");
            try
            {
                Dynamo.Kill();
            }
            catch (Win32Exception)
            {
                Console.WriteLine(Dynamo.StandardError.ReadToEnd());
                throw;
            }
        }
    }
}