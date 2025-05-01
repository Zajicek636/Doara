using System;
using Doara.Ucetnictvi.FakeEntities;
using Doara.Ucetnictvi.Generators;
using TestHelper.Utils;
using Xunit;

namespace Doara.Ucetnictvi.Tests;

public class Subject_Tests : UcetnictviDomainModule
{
    private readonly FakeSubject _data;

    public Subject_Tests()
    {
        _data = RandomFakeEntityGenerator.RandomFakeSubject();
    }
    
    [Fact]
    public void Test_Subject()
    {
        var entity = _data.CreateOriginalEntity();
        _data.CheckIfSame(entity);
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeSubject.MaxNameLength], MemberType = typeof(PropertyTester))]
    public void Test_Subject_Name(string name, bool shouldThrow)
    {
        _data.Name = name;
        _data.TestSetProperty<Entities.Subject, ArgumentException>(shouldThrow, nameof(Entities.Subject.Name));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeSubject.MaxNameLength], MemberType = typeof(PropertyTester))]
    public void Test_Subject_Name_Setter(string name, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Subject, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetName(name);
            _data.Name = name;
            return ci;
        }, shouldThrow, nameof(Entities.Subject.Name));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [true, FakeSubject.MaxIcLength], MemberType = typeof(PropertyTester))]
    public void Test_Subject_Ic(string? ic, bool shouldThrow)
    {
        _data.Ic = ic;
        _data.TestSetProperty<Entities.Subject, ArgumentException>(shouldThrow, nameof(Entities.Subject.Ic));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [true, FakeSubject.MaxIcLength], MemberType = typeof(PropertyTester))]
    public void Test_Subject_Ic_Setter(string? ic, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Subject, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetIc(ic);
            _data.Ic = ic;
            return ci;
        }, shouldThrow, nameof(Entities.Subject.Ic));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [true, FakeSubject.MaxDicLength], MemberType = typeof(PropertyTester))]
    public void Test_Subject_Dic(string? dic, bool shouldThrow)
    {
        _data.Dic = dic;
        _data.TestSetProperty<Entities.Subject, ArgumentException>(shouldThrow, nameof(Entities.Subject.Dic));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [true, FakeSubject.MaxDicLength], MemberType = typeof(PropertyTester))]
    public void Test_Subject_Dic_Setter(string? dic, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Subject, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetDic(dic);
            _data.Dic = dic;
            return ci;
        }, shouldThrow, nameof(Entities.Subject.Dic));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetBooleanPropertyTestData), MemberType = typeof(PropertyTester))]
    public void Test_Subject_IsVatPayer(bool isVatPayer, bool shouldThrow)
    {
        _data.IsVatPayer = isVatPayer;
        _data.TestSetProperty<Entities.Subject, ArgumentException>(shouldThrow, nameof(Entities.Subject.IsVatPayer));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetBooleanPropertyTestData), MemberType = typeof(PropertyTester))]
    public void Test_Subject_IsVatPayer_Setter(bool isVatPayer, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Subject, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetIsVatPayer(isVatPayer);
            _data.IsVatPayer = isVatPayer;
            return ci;
        }, shouldThrow, nameof(Entities.Subject.IsVatPayer));
    }
}