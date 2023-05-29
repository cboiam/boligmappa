namespace Boligmappa.ConsoleBase;

public class Menu
{
    private readonly IDictionary<string, Func<Task>> options = new Dictionary<string, Func<Task>>();

    public async Task Display(int timeout, CancellationTokenSource cancellationTokenSource)
    {
        var menuItems = options.ToList();

        for (int i = 0; i < menuItems.Count; i++)
        {
            Console.WriteLine("{0}. {1}", i + 1, menuItems[i].Key);
        }
        int choice = ReadInt("Choose an option:", min: 1, max: menuItems.Count);

        await menuItems[choice - 1].Value();
    }

    public Menu Add(string option, Func<Task> callback)
    {
        options.Add(option, callback);
        return this;
    }

    public static int ReadInt(string prompt, int min, int max)
    {
        Console.Write(prompt);
        return ReadInt(min, max);
    }

    public static int ReadInt(int min, int max)
    {
        int value = ReadInt();

        while (value < min || value > max)
        {
            Console.WriteLine("Please enter an integer between {0} and {1} (inclusive)", min, max);
            value = ReadInt();
        }

        return value;
    }

    public static int ReadInt()
    {
        string input = Console.ReadLine();
        int value;

        while (!int.TryParse(input, out value))
        {
            Console.WriteLine("Please enter an integer");
            input = Console.ReadLine();
        }

        return value;
    }

}