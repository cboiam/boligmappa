using Boligmappa.Core.Models;
using Entities = Boligmappa.Core.Entities;

namespace Boligmappa.Data.Postgres.Models;

public class User : Model<Entities.User>
{
    public int PostCount { get; set; }
    public int TodoCount { get; set; }
    public string CreditCard { get; set; }
    public string UserName { get; set; }

    public override Entities.User ToEntity()
    {
        var user = new Entities.User(Id)
        {
            CreditCard = CreditCard,
            UserName = UserName
        };
        user.SetPostCount(user.PostCount);
        user.SetTodoCount(user.TodoCount);
        
        return user;
    }
}