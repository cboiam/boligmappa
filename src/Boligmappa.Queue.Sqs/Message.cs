using System.Text.Json;
using System.Text.Json.Nodes;
using Boligmappa.Configuration;

namespace Boligmappa.Queue.Sqs;

public class Message
{
    public string User { get; set; }
    public JsonNode Data { private get; set; }
    public MessageType Type { get; set; }

    public void SetData<T>(T data)
    {
        Data = JsonSerializer.SerializeToNode(data, Configurations.SerializerOptions);
    }

    public T GetData<T>()
    {
        if (Data != null)
            return Data.GetValue<T>();
        return default(T);
    }
}