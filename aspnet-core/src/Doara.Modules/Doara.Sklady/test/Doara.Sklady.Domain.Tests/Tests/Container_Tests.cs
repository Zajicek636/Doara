using System;
using Doara.Sklady.FakeEntities;
using Doara.Sklady.Generators;
using TestHelper.Utils;
using Xunit;

namespace Doara.Sklady.Tests;

public class Container_Tests : SkladyDomainModule
{
    private readonly FakeContainer _data;

    public Container_Tests()
    {
        _data = RandomFakeEntityGenerator.RandomFakeContainer();
    }
    
    [Fact]
    public void Test_Container()
    {
        var entity = _data.CreateOriginalEntity();
        _data.CheckIfSame(entity);
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeContainer.MaxNameLength], MemberType = typeof(PropertyTester))]
    public void Test_Container_Name(string name, bool shouldThrow)
    {
        _data.Name = name;
        _data.TestSetProperty<Entities.Container, ArgumentException>(shouldThrow, nameof(Entities.Container.Name));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeContainer.MaxNameLength], MemberType = typeof(PropertyTester))]
    public void Test_Container_Name_Setter(string name, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Container, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity();
            ci.SetName(name);
            _data.Name = name;
            return ci;
        }, shouldThrow, nameof(Entities.Container.Name));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeContainer.MaxDescriptionLength], MemberType = typeof(PropertyTester))]
    public void Test_Container_Description(string description, bool shouldThrow)
    {
        _data.Description = description;
        _data.TestSetProperty<Entities.Container, ArgumentException>(shouldThrow, nameof(Entities.Container.Description));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeContainer.MaxDescriptionLength], MemberType = typeof(PropertyTester))]
    public void Test_Container_Description_Setter(string description, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Container, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity();
            ci.SetDescription(description);
            _data.Description = description;
            return ci;
        }, shouldThrow, nameof(Entities.Container.Description));
    }
}