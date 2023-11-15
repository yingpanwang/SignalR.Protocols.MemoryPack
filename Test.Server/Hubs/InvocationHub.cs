using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using Test.Common;

namespace Test.Server.Hubs
{
    public class InvocationHub(ILogger<InvocationHub> logger) : Hub
    {
        private static DateTime Now = DateTime.Now;

        private static Timer timer = new Timer((s) =>
        {
            Now = Now.AddSeconds(1);
        }, null, 0, 1);

        public override Task OnConnectedAsync()
        {
            // 可以将连接按名称分组
            Groups.AddToGroupAsync(Context.ConnectionId, "connected");

            logger.LogInformation("{id} 已连接,{date}", Context.ConnectionId, DateTime.Now);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (exception == null)
            {
                // 如果 exception 为空说明客户端主动断开
            }
            logger.LogInformation("{id} 断开链接,{date}", Context.ConnectionId, DateTime.Now);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task Invocation(InvocationObj data)
        {
            await SendCallback("Invocation", JsonSerializer.Serialize(data));
        }

        private Task SendAllCallback(string methodName, string desc)
        {
            return Clients.All.SendAsync("Callback", $"methodName:{methodName},desc:{desc}");
        }

        private Task SendCallback(string methodName, string desc)
        {
            return Clients.Caller.SendAsync("Callback", $"methodName:{methodName},desc:{desc}");
        }
    }
}