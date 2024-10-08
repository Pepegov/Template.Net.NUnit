using System.Data.Common;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Bogus;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Pepegov.UnitOfWork;
using Pepegov.UnitOfWork.MongoDb;
using Template.Net.NUnit.Test.Core.Customizations;
using Template.Net.NUnit.Test.Database;

namespace Template.Net.NUnit.Test.Core;

public abstract class TestBase
{
    private DbConnection _connection = null!;
    protected Faker DataSetFaker { get; private set; } = null!;
    protected IFixture Fixture { get; private set; } = null!;
    protected TestDbContext Context { get; private set; } = null!;
    protected IUnitOfWorkManager UnitOfWorkManager { get; private set; } = null!;
    protected CancellationToken CancellationToken { get; private set; }

    [SetUp]
    protected virtual void Setup()
    {
        //Using in memory Sqlite
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        var options = new DbContextOptionsBuilder()
            .UseSqlite(_connection)
            .Options;

        //Create in memory context & ensure db is created
        Context = new TestDbContext(options);
        Context.Database.EnsureCreated();
        
        CancellationToken = new CancellationToken();
        //Build fixture
        Fixture = new Fixture()
            .Customize(new AutoNSubstituteCustomization())
            .Customize(new AutoMapperCustomization())
            .Customize(new FluentValidationCustomization())
            .Customize(new HttpClientCustomization())
            .Customize(new LoggerCustomization())
            .Customize(new UnitOfWorkCustomization(Context));
        UnitOfWorkManager = Fixture.Freeze<IUnitOfWorkManager>();
    }

    [OneTimeSetUp]
    public virtual void OneTimeSetup()
    {
        DataSetFaker = new Faker();
    }

    [TearDown]
    protected virtual void Teardown()
    {
        _connection.Dispose();
        Context.Dispose();
    }

    /// <summary>
    /// Seed data for testing
    /// </summary>
    /// <param name="data">Data to seed</param>
    /// <typeparam name="T">Type of entity</typeparam>
    protected async Task SeedData<T>(params T[] data)
        where T : class
    {
        await Context.Set<T>().AddRangeAsync(data, CancellationToken);
        await Context.SaveChangesAsync(CancellationToken);
        //TODO: detach only passed as params
        //Detach added entities from context
        foreach (var entry in Context.ChangeTracker.Entries())
        {
            entry.State = EntityState.Detached;
        }
    }
}