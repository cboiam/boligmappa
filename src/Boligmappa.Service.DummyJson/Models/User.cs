using Boligmappa.Core.Models;
using Entities = Boligmappa.Core.Entities;

namespace Boligmappa.Service.DummyJson.Models;

public class User : Model<Entities.User>
{
    public string UserName { get; set; }
    public Bank Bank { get; set; }

    public override Entities.User ToEntity() =>
         new Entities.User(Id)
        {
            UserName = UserName,
            CreditCard = Bank?.CardType
        };
}