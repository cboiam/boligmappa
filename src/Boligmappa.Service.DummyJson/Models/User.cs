using Boligmappa.Core.Models;
using Entities = Boligmappa.Core.Entities;

namespace Boligmappa.Service.DummyJson.Models;

public class User : Model<Entities.User>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Image { get; set; }
    public Bank Bank { get; set; }

    public override Entities.User ToEntity() =>
         new Entities.User(Id)
        {
            FirstName = FirstName,
            LastName = LastName,
            UserName = UserName,
            Email = Email,
            Image = Image,
            CreditCard = Bank?.CardType
        };
}