using Aevo.ChallengeDev.Tests.Core;
using DotNet.Testcontainers.Containers;
using Testcontainers.MsSql;

[assembly: AssemblyFixture(typeof(DependenciesFixture))]

namespace Aevo.ChallengeDev.Tests.Core;

public class DependenciesFixture : IAsyncLifetime
{
    private const string TestDbPassword = "@my-VERY#long-password1234";

    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
        .WithImage("ghcr.io/yanpitangui/mssql-server-fts:latest")
        .WithPassword(TestDbPassword)
        .WithName($"tests-db-{Guid.NewGuid()}")
        .WithCleanUp(true)
        .Build();
    
    public DbContainerConnectionInfo GetDbConnectionInfo()
    {
        if (_msSqlContainer.State != TestcontainersStates.Running)
        {
            throw new InvalidOperationException($"Db {nameof(_msSqlContainer)} is not running.");
        }

        var dataSource = $"{_msSqlContainer.Hostname},{_msSqlContainer.GetMappedPublicPort(1433)}";

        return new DbContainerConnectionInfo(dataSource, "sa", TestDbPassword);
    }
    

    public async ValueTask InitializeAsync() =>
        await Task.WhenAll([
            _msSqlContainer.StartAsync(),
        ]);

    public async ValueTask DisposeAsync()
    {
        await _msSqlContainer.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}

public record DbContainerConnectionInfo(string dataSource, string user, string password);
