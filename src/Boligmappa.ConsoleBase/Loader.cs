namespace Boligmappa.ConsoleBase;

public static class Loader
{
    private static int currentFrame;
    private static char[] frames = new[] { '|', '/', '-', '\\' };
    private static bool stop;

    public static async Task Spin()
    {
        stop = false;

        Console.Clear();
        Console.CursorVisible = false;
        while (!stop)
        {
            UpdateProgress();
            await Task.Delay(100);
        }
        currentFrame = 0;
        Console.CursorVisible = true;
    }

    public static void Stop() => stop = true;

    public static void UpdateProgress()
    {
        var originalX = Console.CursorLeft;
        var originalY = Console.CursorTop;

        Console.Write(frames[currentFrame]);

        currentFrame++;
        if (currentFrame == frames.Length)
        {
            currentFrame = 0;
        }

        Console.SetCursorPosition(originalX, originalY);
    }
}