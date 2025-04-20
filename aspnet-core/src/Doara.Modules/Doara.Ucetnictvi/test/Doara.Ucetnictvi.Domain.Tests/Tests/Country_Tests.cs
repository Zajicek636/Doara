using System;
using Doara.Ucetnictvi.FakeEntities;
using Doara.Ucetnictvi.Generators;
using TestHelper.Utils;
using Xunit;

namespace Doara.Ucetnictvi.Tests;

public class Country_Tests : UcetnictviDomainModule
{
    private readonly FakeCountry _data;

    public Country_Tests()
    {
        _data = RandomFakeEntityGenerator.RandomFakeCountry();
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeCountry.MaxNameLength], MemberType = typeof(PropertyTester))]
    public void Test_Country_Name(string name, bool shouldThrow)
    {
        _data.Name = name;
        _data.TestSetProperty<Entities.Country, ArgumentException>(shouldThrow, nameof(Entities.Country.Name));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeCountry.MaxNameLength], MemberType = typeof(PropertyTester))]
    public void Test_Country_Name_Setter(string name, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Country, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetName(name);
            _data.Name = name;
            return ci;
        }, shouldThrow, nameof(Entities.Country.Name));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeCountry.MaxCodeLength], MemberType = typeof(PropertyTester))]
    public void Test_Country_Code(string code, bool shouldThrow)
    {
        _data.Code = code;
        _data.TestSetProperty<Entities.Country, ArgumentException>(shouldThrow, nameof(Entities.Country.Code));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeCountry.MaxCodeLength], MemberType = typeof(PropertyTester))]
    public void Test_Country_Code_Setter(string code, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Country, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetCode(code);
            _data.Code = code;
            return ci;
        }, shouldThrow, nameof(Entities.Country.Code));
    }
}