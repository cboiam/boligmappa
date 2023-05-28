using Boligmappa.Core.Entities;
using Boligmappa.Core.Handlers;
using Boligmappa.Core.Repositories;
using Boligmappa.Core.Services;
using Microsoft.Extensions.Logging;

namespace Boligmappa.Core.Test.Handlers;

public class UserHandlerTest
{
    private readonly UserHandler instance;
    private readonly Mock<IUserRepository> userRepository;
    private readonly Mock<IUserService> userService;
    private readonly Mock<IPostService> postService;
    private readonly Mock<ITodoService> todoService;

    public UserHandlerTest()
    {
        userRepository = new Mock<IUserRepository>();
        userService = new Mock<IUserService>();
        postService = new Mock<IPostService>();
        todoService = new Mock<ITodoService>();

        instance = new UserHandler(userRepository.Object,
            userService.Object,
            postService.Object,
            todoService.Object,
            Mock.Of<ILogger<UserHandler>>());
    }

    [Fact]
    public async Task GetPopularUsers_EmptyResult_ReturnsEmpty()
    {
        userRepository.Setup(x => x.GetPopularUsers())
            .ReturnsAsync(new List<User>());

        var result = await instance.GetPopularUsers();

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetPopularUsers_PopularUsers_ReturnsUsersWithTodo()
    {
        var users = new List<User>
        {
            new User(1)
            {
                UserName = "username",
                CreditCard = "creditcard"
            }
        };

        var todos = new List<Todo>
        {
            new Todo(1)
            {
                UserId = 1,
                Completed = true,
                Description = "description"
            }
        };

        userRepository.Setup(x => x.GetPopularUsers())
            .ReturnsAsync(users);

        todoService.Setup(x => x.GetTodos())
            .ReturnsAsync(todos);

        var result = await instance.GetPopularUsers();

        result.Should().NotBeEmpty()
            .And.HaveCount(1)
            .And.OnlyContain(x => x.Id == 1 && x.Todos.First().Id == 1);
    }

    [Fact]
    public async Task GetMasterCardUsers_EmptyResult_ReturnsEmpty()
    {
        userRepository.Setup(x => x.GetMasterCardUsers())
            .ReturnsAsync(new List<User>());

        var result = await instance.GetMasterCardUsers();

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetMasterCardUsers_MasterCardUsers_ReturnsUsers()
    {
        var users = new List<User>
        {
            new User(1)
            {
                UserName = "username",
                CreditCard = "creditcard"
            }
        };

        userRepository.Setup(x => x.GetMasterCardUsers())
            .ReturnsAsync(users);

        var result = await instance.GetMasterCardUsers();

        result.Should().NotBeEmpty()
            .And.BeEquivalentTo(users);
    }

    [Fact]
    public async Task StoreUsers_EmptyResult_ReturnsZero()
    {
        userService.Setup(x => x.GetUsers())
            .ReturnsAsync(new List<User>());

        var result = await instance.StoreUsers();

        result.Should().Be(0);
    }

    [Fact]
    public async Task StoreUsers_WithResult_ReturnsTwo()
    {
        var user = new User(1)
        {
            UserName = "username",
            CreditCard = "creditcard"
        };
        var users = new List<User>
        {
            user,
            new User(2)
        };
        userService.Setup(x => x.GetUsers())
            .ReturnsAsync(users);

        var post = new Post(1)
        {
            UserId = 1
        };
        postService.Setup(x => x.GetPosts())
            .ReturnsAsync(new List<Post> { post });

        var todo = new Todo(1)
        {
            UserId = 1
        };
        todoService.Setup(x => x.GetTodos())
            .ReturnsAsync(new List<Todo> { todo });

        var result = await instance.StoreUsers();

        userRepository.Verify(x => x.Save(It.IsAny<User>()), Times.Exactly(2));
        userRepository.Verify(x => x.Save(It.Is<User>(u => u.Id == 1 &&
            u.PostCount == 1 &&
            u.TodoCount == 1)));
            
        result.Should().Be(2);
    }
}