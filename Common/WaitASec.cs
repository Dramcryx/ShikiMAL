namespace Common;

/// <summary>
/// Вспомогательная палочка для задержки между асинхронными вызовами
/// </summary>
public class WaitASec
{
    public static async Task RunAsync(Func<Task> func, int waitMs = 1000)
    {
        int ticks = Environment.TickCount;
        await func();
        int ticksToDelay = waitMs - (Environment.TickCount - ticks);
        if (ticksToDelay > 0)
            await Task.Delay(ticksToDelay);
    }

    public static async Task<T> RunAsync<T>(Func<Task<T>> func, int waitMs = 1000)
    {
        int ticks = Environment.TickCount;
        var result = await func();
        int ticksToDelay = waitMs - (Environment.TickCount - ticks);
        if (ticksToDelay > 0)
            await Task.Delay(ticksToDelay);

        return result;
    }
}
