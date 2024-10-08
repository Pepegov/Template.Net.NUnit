using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Template.Net.NUnit.Test.Core.Specimens;

namespace Template.Net.NUnit.Test.Core.Customizations;
/// <summary>
/// A Customization that add UnitOfWork builder to Fixture
/// </summary>
public sealed class UnitOfWorkCustomization : ICustomization
{
    private readonly DbContext _context;

    public UnitOfWorkCustomization(DbContext context)
    {
        _context = context;
    }

    public void Customize(IFixture fixture)
    {
        fixture.Customizations.Add(new UnitOfWorkSpecimenBuilder(_context));
    }
}