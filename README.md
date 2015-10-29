#LocalDynamoDb

This nuget provides a wrapper for the DynamoDb.jar that is available from AWS for local deployment. 
It should be downloadable from here - http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Tools.DynamoDBLocal.html

##Getting Started

The DynamoDb.jar isn't included in this nuget to avoid licenseing issues.  
Add the .jar file to your project in a folder called "dynamodblocal".
Add is dependency libraries to a subfolder called "DynamoDBLocal_lib".

Set the .jar and all of the dependency libraries to copy to the output directory.

To spin up an instance of dynamo

```csharp
var localDynamo = new LocalDynamo();
localDynamo.Start();
```

And to stop it
```csharp
localDynamo.Stop();
```

The local dynamo will start on localhost:8000

##Using the Local Dynamo
To have your tests point at your local instance of dynamo you will have to override the ServiceUrl.

```csharp
var config = new AmazonDynamoDBConfig { ServiceURL = "http://localhost:8000" };
var credentials = new BasicAWSCredentials("CREDENTIALS");
var client = new AmazonDynamoDBClient(credentials, config);
```
