#LocalDynamoDb

This nuget provides a wrapper for the DynamoDb.jar that is available from AWS for local deployment. 
It should be downloadable from here - http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Tools.DynamoDBLocal.html

##Getting Started

Java is required as Amazon provide DYnamoDb as a jar file.

The DynamoDb.jar isn't included in this nuget to avoid licenseing issues.  
Add the .jar file to your project in a folder called "dynamodblocal".
Add is dependency libraries to a subfolder called "DynamoDBLocal_lib".

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

The local dynamo will start on the port specified in the constructir, or will default to 8000 if no port is specified.

##Using the Local Dynamo
The instance of LocalDynamo will have a property called Client, which is a DynamoDb client that is pointed at the correct url.
