using Dashome.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();
await app.UseApiAsync();
app.Run();