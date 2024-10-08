using AutoFixture;
using Template.Net.NUnit.Test.Core.Specimens;
    
namespace Template.Net.NUnit.Test.Core.Customizations;
/// <summary>
/// A Customization that add AutoMapper builder to Fixture
/// </summary>
public sealed class AutoMapperCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customizations.Add(new AutoMapperSpecimenBuilder());
    }
}