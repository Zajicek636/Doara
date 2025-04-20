using System;
using Doara.Ucetnictvi.FakeEntities;
using Doara.Ucetnictvi.Generators;
using TestHelper.Utils;
using Xunit;

namespace Doara.Ucetnictvi.Tests;

public class Address_Tests : UcetnictviDomainModule
{
    private readonly FakeAddress _data;

    public Address_Tests()
    {
        _data = RandomFakeEntityGenerator.RandomFakeAddress();
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeAddress.MaxStreetLength], MemberType = typeof(PropertyTester))]
    public void Test_Address_Name(string street, bool shouldThrow)
    {
        _data.Street = street;
        _data.TestSetProperty<Entities.Address, ArgumentException>(shouldThrow, nameof(Entities.Address.Street));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeAddress.MaxStreetLength], MemberType = typeof(PropertyTester))]
    public void Test_Address_Name_Setter(string street, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Address, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetStreet(street);
            _data.Street = street;
            return ci;
        }, shouldThrow, nameof(Entities.Address.Street));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeAddress.MaxCityLength], MemberType = typeof(PropertyTester))]
    public void Test_Address_City(string city, bool shouldThrow)
    {
        _data.City = city;
        _data.TestSetProperty<Entities.Address, ArgumentException>(shouldThrow, nameof(Entities.Address.City));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeAddress.MaxCityLength], MemberType = typeof(PropertyTester))]
    public void Test_Address_City_Setter(string city, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Address, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetCity(city);
            _data.City = city;
            return ci;
        }, shouldThrow, nameof(Entities.Address.City));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeAddress.MaxPostalCodeLength], MemberType = typeof(PropertyTester))]
    public void Test_Address_PostalCode(string postalCode, bool shouldThrow)
    {
        _data.PostalCode = postalCode;
        _data.TestSetProperty<Entities.Address, ArgumentException>(shouldThrow, nameof(Entities.Address.PostalCode));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeAddress.MaxPostalCodeLength], MemberType = typeof(PropertyTester))]
    public void Test_Address_PostalCode_Setter(string postalCode, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Address, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetPostalCode(postalCode);
            _data.PostalCode = postalCode;
            return ci;
        }, shouldThrow, nameof(Entities.Address.PostalCode));
    }
}