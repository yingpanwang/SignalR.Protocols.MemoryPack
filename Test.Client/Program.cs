// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.SignalR.Client;
using SignalR.Protocols.MemoryPack;
using System.Text;
using System.Threading.Channels;
using Test.Common;

Console.WriteLine("Hello, World!");

var hubConnection = new HubConnectionBuilder()
    //.WithUrl("https://localhost:7027/asyncEnumerableHub")
    .WithUrl("https://localhost:7027/channelHub")
    // .WithUrl("https://localhost:7027/invocationHub", opt =>
    // {
    //     //opt.AccessTokenProvider = () =>
    //     //{
    //     //    return Task.FromResult<string?>("");
    //     //};
    //     opt.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
    // })
    .WithAutomaticReconnect(new RetryPolicy())
    .AddMemoryPackProtocol()
    .Build();

await hubConnection.StartAsync();

hubConnection.Reconnected += async (s) =>
{
    await Console.Out.WriteLineAsync("已重连");
};

hubConnection.Reconnecting += async (e) =>
{
    await Console.Out.WriteLineAsync("重连中!");
};

hubConnection.On<string>("Callback", desc =>
{
    Console.WriteLine(desc);
});

hubConnection.On<string>("Weather", Console.WriteLine);

//var send = TestSendInvocation(hubConnection);
//var enumerable = TestAsyncEnumerable(hubConnection);
await TestChannel(hubConnection);
//await Task.WhenAll(enumerable);
//await Task.WhenAll(send, enumerable);

static async Task TestSendInvocation(HubConnection hubConnection)
{
    ConsoleKey key;
    do
    {
        key = Console.ReadKey().Key;

        await hubConnection.SendAsync("Invocation", new InvocationObj());
    } while (key != ConsoleKey.Escape);
}

static async Task TestChannel(HubConnection hubConnection)
{
    ConsoleKey key;

    var channel = Channel.CreateUnbounded<UploadBlock>();

    await hubConnection.SendAsync("UploadStream", channel.Reader);

    do
    {
        key = Console.ReadKey().Key;

        await channel.Writer.WriteAsync(new UploadBlock()
        {
            Name = key.ToString(),
            Data = Encoding.UTF8.GetBytes(key.ToString())
        });
    } while (key != ConsoleKey.Escape);

    channel.Writer.Complete();
}

static async Task TestAsyncEnumerable(HubConnection hubConnection)
{
    var stream = hubConnection.StreamAsync<DateTime>("SyncDate");

    await foreach (var date in stream)
    {
        Console.WriteLine("stream date :" + date);
    }
}

internal class RetryPolicy : IRetryPolicy
{
    public TimeSpan? NextRetryDelay(RetryContext retryContext)
    {
        Console.WriteLine($"已重试次数:{retryContext.PreviousRetryCount}");
        return TimeSpan.FromSeconds(3);
    }
}