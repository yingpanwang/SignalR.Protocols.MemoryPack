using SignalR.Protocols.MemoryPack;
using Test.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services
    .AddSignalR()
    .AddMemoryPackProtocol();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapHub<InvocationHub>("/invocationHub");

app.MapHub<AsyncEnumerableHub>("/asyncEnumerableHub");

app.MapHub<ChannelHub>("/channelHub");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();