using Boligmappa.Server;
using Boligmappa.Queue.Sqs;
using Boligmappa.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configuration = Configurations.BuildConfiguration();

builder.Logging.BuildLogger(configuration)
    .AddConsole();

builder.Configuration.AddConfiguration(configuration);

builder.Services.AddLogging()
    .AddSqs(configuration);

builder.Services.AddSignalR();

var app = builder.Build();
app.MapHub<AppHub>("/app");
app.Run();
