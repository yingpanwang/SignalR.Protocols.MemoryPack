using Microsoft.AspNetCore.SignalR;

namespace Test.Server.Hubs;

public class AsyncEnumerableHub : Hub
{
    public async IAsyncEnumerable<DateTime> SyncDate()
    {
        await Task.Yield();

        while (true)
        {
            yield return DateTime.Now;

            await Task.Delay(1000);
        }
    }
}