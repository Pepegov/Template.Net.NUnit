using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Template.Net.NUnit.Test.Core.DbConverters;

namespace Template.Net.NUnit.Test.Database;

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties())
            {
                // JSON conversion for dictionary and list types
                if (property.ClrType == typeof(Dictionary<string, string>))
                {
                    property.SetValueConverter(new EntityToJsonConverter<Dictionary<string, string>>());
                }
                // TODO add if you need
                // else if (property.ClrType == typeof(List<string>))
                // {
                //     property.SetValueConverter(new EntityToJsonConverter<List<string>>());
                // }

                // Decimal type conversion for SQLite
                if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
                {
                    property.SetValueConverter(
                        new ValueConverter<decimal, double>(
                            to => Convert.ToDouble(to),
                            from => Convert.ToDecimal(from)));
                }

                // DateTimeOffset conversion for SQLite
                if (property.ClrType == typeof(DateTimeOffset) || property.ClrType == typeof(DateTimeOffset?))
                {
                    property.SetValueConverter(
                        new ValueConverter<DateTimeOffset, DateTime>(
                            to => to.UtcDateTime,
                            from => DateTime.SpecifyKind(from, DateTimeKind.Utc)));
                }
            }
        }
    }
}