var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

DotNetEnv.Env.Load();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwaggerUi(options =>
{
    options.DocumentPath = "/openapi/v1.json";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
