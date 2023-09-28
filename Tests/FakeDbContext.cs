using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Repository;

namespace ProvaPub.Tests;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class FakeDbContext : IDisposable
{
    protected readonly TestDbContext _ctx;
    protected FakeDbContext()
    {
        _ctx = new TestDbContext(ObterConfiguracoes());
    }

    private static DbContextOptions<TestDbContext> ObterConfiguracoes()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        var builder = new DbContextOptionsBuilder<TestDbContext>();
        builder.UseInMemoryDatabase("DbTeste")
            .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
    }

    public void Dispose()
    {
        _ctx.Dispose();
        GC.SuppressFinalize(this);
    }

    private string? GetDebuggerDisplay()
    {
        return ToString();
    }
}