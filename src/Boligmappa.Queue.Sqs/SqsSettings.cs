namespace Boligmappa.Queue.Sqs;

public class SqsSettings
{
    public string QueueUrl { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string Region { get; set; }
}