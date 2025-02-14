using System.Net.Http.Headers;
using Aevo.ChallengeDev.WebApi.Core.Auth;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;

namespace Aevo.ChallengeDev.Tests.Core;

public abstract class TestBase(IntegrationTestFactory factory) : IAsyncLifetime, IClassFixture<IntegrationTestFactory>
{
    protected readonly IntegrationTestFactory Factory = factory;
    protected readonly HttpClient Http = factory.CreateClient();
    protected FakeTimeProvider FakeTimeProvider = factory.Services.GetRequiredService<FakeTimeProvider>();

    protected virtual async Task Seed()
    {
        var context = Factory.GetDbContext();

        context.Usuarios.AddRange(UsuariosDeTestePredefinidos.ObterTodosOsUsuarios());
        context.Salas.AddRange(SalasDeTestePredefinidas.ObterTodasAsSalas());
        
        await context.SaveChangesAsync();
    }

    protected void LoginAs(UsuarioDeTeste usuario)
    {
        var authService = Factory.Services.GetRequiredService<IAuthService>();
        var token = authService.GenerateAccessToken(usuario.Id);
        
        Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async ValueTask InitializeAsync()
    {
        Http.DefaultRequestHeaders.Authorization = null;
        await Factory.ResetDatabaseAsync();

        await Seed();
    }

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}