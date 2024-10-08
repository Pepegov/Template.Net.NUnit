using AutoFixture;
using Template.Net.NUnit.Test.Core;

namespace Template.Net.NUnit.Test.Tests;

public class TemplateTest : TestBase
{
    private TemplateSut _sut = null!;
    
    protected override void Setup()
    {
        base.Setup();
        _sut = Fixture.Create<TemplateSut>();
    }

    [Test]
    public void IsSutGetTrue()
    {
        // Arrange
        _sut.SetValue(true);
        
        // Act
        var value = _sut.GetValue();
        
        // Assert
        Assert.That(value);
    }
}

internal class TemplateSut
{
    private bool value;
    public void SetValue(bool value) => this.value = value;
    public bool GetValue() => value;
}