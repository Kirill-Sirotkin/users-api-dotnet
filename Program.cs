using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using users_api_dotnet.DatabaseContext;
using users_api_dotnet.Services;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataBaseContext>(o => o.UseNpgsql(DotNetEnv.Env.GetString("DATABASE_URL")));
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o => {
        o.TokenValidationParameters = new TokenValidationParameters() {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(DotNetEnv.Env.GetString("JWT_SECRET"))),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateActor = false,
            ClockSkew = TimeSpan.FromMinutes(0),
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo {
        Title = "My API",
        Version = "v1"
    });

    var securityScheme = new OpenApiSecurityScheme {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT in the following format: Bearer [token]"
    };
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {{
        new OpenApiSecurityScheme {
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            }
        },
        new string[] {}
    }});

    options.AddSecurityDefinition("Bearer", securityScheme);
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
