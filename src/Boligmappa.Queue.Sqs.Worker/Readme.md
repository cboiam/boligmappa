# AWS Lambda Simple SQS Function Project

This starter project consists of:
* Function.cs - class file containing a class with a single function handler method
* aws-lambda-tools-defaults.json - default argument settings for use with Visual Studio and command line deployment tools for AWS

The generated function handler responds to events on an Amazon SQS queue.

After deploying your function you must configure an Amazon SQS queue as an event source to trigger your Lambda function.

## Here are some steps to follow to get started from the command line:

Once you have edited your template and code you can deploy your application using the [Amazon.Lambda.Tools Global Tool](https://github.com/aws/aws-extensions-for-dotnet-cli#aws-lambda-amazonlambdatools) from the command line.

Install Amazon.Lambda.Tools Global Tools if not already installed.
```
    dotnet tool install -g Amazon.Lambda.Tools
```

If already installed check if new version is available.
```
    dotnet tool update -g Amazon.Lambda.Tools
```

Deploy function to AWS Lambda
```
    cd "Boligmappa.Queue.Sqs.Worker/src/Boligmappa.Queue.Sqs.Worker"
    dotnet lambda deploy-function
```
