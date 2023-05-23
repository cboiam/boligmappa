namespace Boligmappa.ConsoleBase;

public class Loader
{
    private int currentFrame;
    private char[] frames = new[] { '|', '/', '-', '\\' };

    public async Task Spin(CancellationToken cancellationToken)
    {
        Console.Clear();
        Console.CursorVisible = false;
        while (!cancellationToken.IsCancellationRequested)
        {
            UpdateProgress();
            await Task.Delay(100);
        }
        currentFrame = 0;
        Console.CursorVisible = true;
    }

    public void UpdateProgress()
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