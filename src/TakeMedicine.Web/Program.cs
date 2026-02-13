using TakeMedicine.Web;

var builder = WebApplication.CreateBuilder(args);

// DI
builder.Services.ConfigureApp(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();