using ProxyServer.Controller;
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:4200", "http://localhost:5078");
                      });
});

// Register HttpClient and ApiService
builder.Services.AddHttpClient<ApiService>();
builder.Services.AddControllers();

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();
app.Run();
