using System.Text;
using Aevo.ChallengeDev.WebApi;
using Aevo.ChallengeDev.WebApi.Core;
using Aevo.ChallengeDev.WebApi.Core.Auth;
using Aevo.ChallengeDev.WebApi.Modulos.Agendamentos;
using Aevo.ChallengeDev.WebApi.Modulos.Salas;
using Aevo.ChallengeDev.WebApi.Modulos.Usuarios;
using Aevo.ChallengeDev.WebApi.Modulos.Usuarios.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Db"))
#if DEBUG
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
#endif
        ;
});

builder.Services.AddSingleton(TimeProvider.System);

builder.Services.Configure<AuthConfig>(builder.Configuration.GetSection("Auth"));

builder.Services
    .AddAuthentication(AppAuthDefaults.AuthenticationScheme)
    .AddScheme<AppAuthOptions, AppAuthAuthenticationHandler>(AppAuthDefaults.AuthenticationScheme, options => { });

builder.Services.AddAuthorization(options =>
{
    options.InvokeHandlersAfterFailure = false;

    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddScoped<IAuthService, AuthService>();

var handlers = typeof(IAssemblyMarker).Assembly
    .GetTypes()
    .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                   type.GetInterface(typeof(ICaseHandler<,>).Name) != null)
    .ToArray();

foreach (var handler in handlers)
    builder.Services.AddScoped(handler);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapUsuariosEndpoints();
app.MapSalasEndpoints();
app.MapAgendamentosEndpoints();

app.Run();