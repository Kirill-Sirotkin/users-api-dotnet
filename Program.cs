using Microsoft.EntityFrameworkCore;
using users_api_dotnet.DatabaseContext;
using users_api_dotnet.Services;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataBaseContext>(o => o.UseNpgsql(DotNetEnv.Env.GetString("DATABASE_URL")));
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IUsersService, UsersService>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}
app.UseSwaggerUi(options => {
    options.DocumentPath = "/openapi/v1.json";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
