var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Create a builder for the web application.
var builder = WebApplication.CreateBuilder(args);

// Configure CORS to allow specific origins.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200");
                      });
});

// Register HttpClient and ApiService
builder.Services.AddHttpClient<ApiService>();
builder.Services.AddControllers();

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

// Map controller endpoints to routes.
app.MapControllers();

app.Run();
