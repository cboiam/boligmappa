using Boligmappa.Core.Models;
using Newtonsoft.Json;
using Entities = Boligmappa.Core.Entities;

namespace Boligmappa.Service.DummyJson.Models;

public class Todo : Model<Entities.Todo>
{    
    [JsonProperty("Todo")] 
    public string Description { get; set; }
    public string Completed { get; set; }
    public int UserId { get; set; }

    public override Entities.Todo ToEntity() =>
        new Entities.Todo(Id)
        {
            Description = Description,
            Completed = Completed,
            UserId = UserId
        };
}