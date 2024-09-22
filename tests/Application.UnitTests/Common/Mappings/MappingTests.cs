using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using Demo.Application.Common.Interfaces;
using Demo.Application.Common.Models;
using Demo.Application.Customers.Queries.GetCustomers; 
using Demo.Domain.Entities;
using NUnit.Framework;

namespace Demo.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        _mapper = _configuration.CreateMapper();
    }
     

    [Test]
    [TestCase(typeof(Customer), typeof(CustomerDto))]  
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
