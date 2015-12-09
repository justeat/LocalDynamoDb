#LocalDynamoDb

This nuget provides a wrapper for the DynamoDb.jar that is available from AWS for local deployment. 
It should be downloadable from here - http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Tools.DynamoDBLocal.html

##Getting Started

Java is required as Amazon provide the local DynamoDb as a jar file.

The DynamoDb.jar isn't included in this nuget to avoid licensing issues.  
Add the .jar file to your project in a folder called "dynamodblocal".
Add its dependency libraries to a subfolder of dynamodblocal called "DynamoDBLocal_lib".

Set the .jar and all of the dependency libraries to copy to the output directory.

To spin up an instance of dynamo

```csharp
var localDynamo = new LocalDynamo(8080);
localDynamo.Start();
```

And to stop it
```csharp
localDynamo.Stop();
```

The local dynamo will start on the port specified in the constructor, or will default to 8000 if no port is specified.

##Using the Local Dynamo
The instance of LocalDynamo will have a property called Client, which is a DynamoDb client that is pointed at the correct url.

If you wish to create your own client, you will have to set the ServiceURL of the client to point at your local dynamo.

``` csharp
var config = new AmazonDynamoDBConfig { ServiceURL = String.Format("http://localhost:8000"};
var credentials = new BasicAWSCredentials("CRED", "ENTIALS");
var client = new AmazonDynamoDBClient(credentials, config);
```
## Potential Issues 
### Resharper

To run the tests through resharper, you will have to go into the options and under unit tests, disable the Shadow Copy option.

### Visual Studio

To be able to start processes you will need to run visual studio in Administrator mode.
