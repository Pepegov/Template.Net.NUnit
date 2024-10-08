using AutoFixture.Kernel;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Template.Net.NUnit.Test.Core.Specimens;

public class FluentValidationSpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is not Type type || !type.IsGenericType ||  type.GetGenericTypeDefinition() != typeof(IValidator<>))
        {
            return new NoSpecimen(); // indicate that current specimen builder cannot create requested object
        }

        var collection = new ServiceCollection();
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        collection.AddValidatorsFromAssemblies(assemblies);
        var provider = collection.BuildServiceProvider();
        return provider.GetRequiredService(type);
    }
}