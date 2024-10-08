using AutoFixture.Kernel;
using AutoMapper;

namespace Template.Net.NUnit.Test.Core.Specimens;
/// <summary>
/// Fixture automapper builder
/// </summary>
public sealed class AutoMapperSpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        //If requested is not match required type then skip this builder
        if (request is not Type type || type != typeof(IMapper))
        {
            return new NoSpecimen(); // indicate that current specimen builder cannot create requested object
        }
        //build configuration
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(assemblies);
        });
        return configuration.CreateMapper();
    }
}