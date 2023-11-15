using System.Threading.Channels;
using Microsoft.AspNetCore.SignalR;
using Test.Common;

namespace Test.Server.Hubs;

public class ChannelHub:Hub
{
    public async Task UploadStream(ChannelReader<UploadBlock> reader)
    {
        await foreach (var i in reader.ReadAllAsync())
        {
           await Clients.Caller.SendAsync("Callback",$"读取:{i.Name},长度:{i.Data.Length}");
        }
    }
}