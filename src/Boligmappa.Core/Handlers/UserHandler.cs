using Boligmappa.Core.Entities;
using Boligmappa.Core.Handlers.Abstractions;
using Boligmappa.Core.Repositories;
using Boligmappa.Core.Services;
using Microsoft.Extensions.Logging;

namespace Boligmappa.Core.Handlers;

public class UserHandler : IUserHandler
{
    private readonly IUserRepository userRepository;
    private readonly IUserService userService;
    private readonly IPostService postService;
    private readonly ITodoService todoService;
    private readonly ILogger<UserHandler> logger;

    public UserHandler(IUserRepository userRepository, IUserService userService, IPostService postService, ITodoService todoService, ILogger<UserHandler> logger)
    {
        this.userRepository = userRepository;
        this.todoService = todoService;
        this.userService = userService;
        this.logger = logger;
        this.postService = postService;
    }

    public async Task<IEnumerable<User>> GetPopularUsers()
    {
        logger.LogInformation("Loading popular users");
        var popularUsersTask = userRepository.GetPopularUsers();

        logger.LogDebug("Loading todos");
        var todosTask = todoService.GetTodos();

        IEnumerable<User> popularUsers = await popularUsersTask;
        foreach (var user in popularUsers)
        {
            var userTodos = (await todosTask)
                .Where(t => t.UserId == user.Id);
            
            user.SetTodos(userTodos);
        }
        logger.LogInformation("Popiular users loaded: {count}", popularUsers.Count());

        return popularUsers;
    }

    public async Task<IEnumerable<User>> GetMasterCardUsers()
    {
        logger.LogInformation("Loading master card users");
        var users = await userRepository.GetMasterCardUsers();
        logger.LogInformation("Master card users loaded: {count}", users.Count());

        return users;
    }

    public async Task<int> StoreUsers()
    {
        logger.LogInformation("Loading users");
        var usersTask = userService.GetUsers();

        logger.LogDebug("Loading posts");
        var postsTask = postService.GetPosts();

        logger.LogDebug("Loading todos");
        var todosTask = todoService.GetTodos();

        foreach (var user in await usersTask)
        {
            var postsCount = (await postsTask).Where(p => p.UserId == user.Id).Count();
            var todosCount = (await todosTask).Where(t => t.UserId == user.Id).Count();

            user.SetPostCount(postsCount);
            user.SetTodoCount(todosCount);

            logger.LogDebug("Saving user {id}", user.Id);
            await userRepository.Save(user);

            logger.LogDebug("User loaded and saved in database: {user}", user);
        }

        int userCount = (await usersTask).Count();
        logger.LogInformation("Users loaded: {count}", userCount);
        return userCount;
    }
}