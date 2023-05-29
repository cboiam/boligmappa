using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Boligmappa.Core.Queues;
using Boligmappa.Queue.Sqs.Queues;
using Boligmappa.Queue.Sqs.Queues.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Boligmappa.Queue.Sqs;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSqs(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SqsSettings>(options =>
            configuration.GetSection(nameof(SqsSettings))
                .Bind(options));

        services.AddSingleton<IAmazonSQS>(provider =>
        {
            var settings = provider.GetRequiredService<IOptions<SqsSettings>>().Value;
            var accessKey = configuration.GetValue<string>("AccessKey");
            var secretKey = configuration.GetValue<string>("SecretKey");
            var region = RegionEndpoint.GetBySystemName(settings.Region);

            var credentials = new BasicAWSCredentials(accessKey, secretKey);

            return new AmazonSQSClient(credentials, region);
        });

        services.AddScoped<ISqsQueue, SqsQueue>()
            .AddScoped<IUserQueue, UserQueue>();

        return services;
    }
}