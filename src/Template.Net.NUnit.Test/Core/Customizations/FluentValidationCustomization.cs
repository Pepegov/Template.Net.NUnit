using AutoFixture;
using Template.Net.NUnit.Test.Core.Specimens;

namespace Template.Net.NUnit.Test.Core.Customizations;

public class FluentValidationCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customizations.Add(new FluentValidationSpecimenBuilder());
    }
}