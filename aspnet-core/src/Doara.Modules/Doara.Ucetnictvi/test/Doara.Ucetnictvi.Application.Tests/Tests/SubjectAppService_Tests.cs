using System;
using System.Linq;
using System.Threading.Tasks;
using Doara.Ucetnictvi.FakeEntities;
using Doara.Ucetnictvi.IAppServices;
using Doara.Ucetnictvi.Utils.Converters;
using Shouldly;
using TestHelper.Generators;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Volo.Abp.Validation;
using Xunit;

namespace Doara.Ucetnictvi.Tests;

public class SubjectAppService_Tests : UcetnictviApplicationTestBase<UcetnictviApplicationTestModule>
{
    private readonly ISubjectAppService _subjectAppService;
    private readonly IAddressAppService _addressAppService;
    private readonly IGuidGenerator _guidGenerator;
    
    public SubjectAppService_Tests()
    {
        _subjectAppService = GetRequiredService<ISubjectAppService>();
        _guidGenerator = GetRequiredService<IGuidGenerator>();
        _addressAppService = GetRequiredService<IAddressAppService>();
    }
    
    [Fact]
    public async Task Should_Get_Subject()
    {
        var czSubject = await _subjectAppService.GetAsync(TestData.CzSubjectId);
        czSubject.Id.ShouldBe(TestData.CzSubjectId);
        czSubject.Name.ShouldBe("Alza.cz a.s.");
        czSubject.Ic.ShouldBe("27082440");
        czSubject.Dic.ShouldBe("CZ27082440");
        czSubject.IsVatPayer.ShouldBeTrue();
        czSubject.Address.Id.ShouldBe(TestData.CzAddressId);
        czSubject.Address.Street.ShouldBe("Václavské náměstí 1");
        czSubject.Address.City.ShouldBe("Praha");
        czSubject.Address.PostalCode.ShouldBe("11000");
        czSubject.Address.Country.Id.ShouldBe(TestData.CzCountryId);
        czSubject.Address.Country.Code.ShouldBe("CZ");
        czSubject.Address.Country.Name.ShouldBe("Česká republika");
    }
    
    [Fact]
    public async Task Should_Throw_Get_Non_Existing_Subject()
    {
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _subjectAppService.GetAsync(id);
        });
        exception.Message.ShouldContain(nameof(Entities.Subject));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Create_Subject()
    {
        var input = Converter.CreateSubjectInput(TestData.CzAddressId);
        var subject = await _subjectAppService.CreateAsync(input);
        
        subject.Id.ShouldNotBe(Guid.Empty);
        subject.Name.ShouldBe(input.Name);
        subject.Ic.ShouldBe(input.Ic);
        subject.Dic.ShouldBe(input.Dic);
        subject.IsVatPayer.ShouldBe(input.IsVatPayer);
        subject.Address.Id.ShouldBe(TestData.CzAddressId);
        subject.Address.Street.ShouldBe("Václavské náměstí 1");
        subject.Address.City.ShouldBe("Praha");
        subject.Address.PostalCode.ShouldBe("11000");
        subject.Address.Country.Id.ShouldBe(TestData.CzCountryId);
        subject.Address.Country.Code.ShouldBe("CZ");
        subject.Address.Country.Name.ShouldBe("Česká republika");
    }
    
    [Fact]
    public async Task Should_Throw_Create_Subject_Non_Existing_Address()
    {
        var id = _guidGenerator.Create();
        var input = Converter.CreateSubjectInput(id);
        
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
           await _subjectAppService.CreateAsync(input);
        });
        exception.Message.ShouldContain(nameof(Entities.Address));
        exception.Message.ShouldContain(id.ToString());
    }

    [Fact]
    public async Task Should_Update_Subject()
    {
        var subject = await _subjectAppService.GetAsync(TestData.CzSubjectId);
        subject.Name = Converter.DefaultSubjectName;
        subject.Ic = Converter.DefaultSubjectIc;
        subject.Dic = Converter.DefaultSubjectDic;
        subject.IsVatPayer = Converter.DefaultSubjectIsVatPayer;
        subject.Address.Id = TestData.UsAddressId;
        
        var updatedSubject = await _subjectAppService.UpdateAsync(Converter.Convert2UpdateInput(subject));
        
        updatedSubject.Id.ShouldBe(subject.Id);
        updatedSubject.Name.ShouldBe(subject.Name);
        updatedSubject.Ic.ShouldBe(subject.Ic);
        updatedSubject.Dic.ShouldBe(subject.Dic);
        updatedSubject.IsVatPayer.ShouldBe(subject.IsVatPayer);
        updatedSubject.Address.Id.ShouldBe(TestData.UsAddressId);
        updatedSubject.Address.Street.ShouldBe("1600 Pennsylvania Avenue NW");
        updatedSubject.Address.City.ShouldBe("Washington");
        updatedSubject.Address.PostalCode.ShouldBe("20500");
        updatedSubject.Address.Country.Id.ShouldBe(TestData.UsCountryId);
        updatedSubject.Address.Country.Code.ShouldBe("USA");
        updatedSubject.Address.Country.Name.ShouldBe("United States");
    }
    
    [Fact]
    public async Task Should_Throw_Update_Non_Existing_Subject()
    {
        var id = _guidGenerator.Create();
        var subject = await _subjectAppService.GetAsync(TestData.CzSubjectId);
        subject.Name = Converter.DefaultSubjectName;
        subject.Ic = Converter.DefaultSubjectIc;
        subject.Dic = Converter.DefaultSubjectDic;
        subject.IsVatPayer = Converter.DefaultSubjectIsVatPayer;
        subject.Address.Id = TestData.UsAddressId;
        subject.Id = id;
        
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _subjectAppService.UpdateAsync(Converter.Convert2UpdateInput(subject));
        });
        exception.Message.ShouldContain(nameof(Entities.Subject));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Throw_Update_Non_Existing_Address()
    {
        var id = _guidGenerator.Create();
        var subject = await _subjectAppService.GetAsync(TestData.CzSubjectId);
        subject.Address.Id = id;
        subject.Name = Converter.DefaultSubjectName;
        subject.Ic = Converter.DefaultSubjectIc;
        subject.Dic = Converter.DefaultSubjectDic;
        subject.IsVatPayer = Converter.DefaultSubjectIsVatPayer;
        
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _subjectAppService.UpdateAsync(Converter.Convert2UpdateInput(subject));
        });
        exception.Message.ShouldContain(nameof(Entities.Address));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Delete_Subject()
    {
        await _subjectAppService.DeleteAsync(TestData.CzSubjectId);

        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _subjectAppService.GetAsync(TestData.CzSubjectId);
        });
        exception.Message.ShouldContain(nameof(Entities.Subject));
        exception.Message.ShouldContain(TestData.CzSubjectId.ToString());
    }
    
    [Fact]
    public async Task Should_Throw_Delete_Non_Existing_Subject()
    {
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _subjectAppService.DeleteAsync(id);
        });
        exception.Message.ShouldContain(nameof(Entities.Subject));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_GetAll()
    {
        var entities = await _subjectAppService.GetAllAsync(new PagedAndSortedResultRequestDto
        {
            SkipCount = 1,
            MaxResultCount = 4,
            Sorting = "Name"
        });
        
        entities.TotalCount.ShouldBe(3);
        entities.Items.Count.ShouldBe(2);
        entities.Items[0].Id.ShouldBe(TestData.UsSubjectId);
        entities.Items[1].Id.ShouldBe(TestData.SkSubjectId);
        entities.Items[0].AddressId.ShouldBe(TestData.UsAddressId);
        entities.Items[1].AddressId.ShouldBe(TestData.SkAddressId);
    }
    
    [Fact]
    public async Task Should_GetAllWithDetail()
    {
        var entities = await _subjectAppService.GetAllWithDetailAsync(new PagedAndSortedResultRequestDto
        {
            SkipCount = 1,
            MaxResultCount = 4,
            Sorting = "Name"
        });
        
        entities.TotalCount.ShouldBe(3);
        entities.Items.Count.ShouldBe(2);
        entities.Items[0].Id.ShouldBe(TestData.UsSubjectId);
        entities.Items[1].Id.ShouldBe(TestData.SkSubjectId);
        entities.Items[0].Address.Id.ShouldBe(TestData.UsAddressId);
        entities.Items[1].Address.Id.ShouldBe(TestData.SkAddressId);
        entities.Items[0].Address.Country.Id.ShouldBe(TestData.UsCountryId);
        entities.Items[1].Address.Country.Id.ShouldBe(TestData.SkCountryId);
    }

    [Fact]
    public async Task Should_CreateWithAddress()
    {
        var addressInput = Converter.CreateAddressInput(TestData.CzCountryId);
        var input = Converter.CreateSubjectWithAddressInput(addressInput);
        var subject = await _subjectAppService.CreateWithAddressAsync(input);
        
        subject.Id.ShouldNotBe(Guid.Empty);
        subject.Name.ShouldBe(input.Name);
        subject.Ic.ShouldBe(input.Ic);
        subject.Dic.ShouldBe(input.Dic);
        subject.IsVatPayer.ShouldBe(input.IsVatPayer);
        subject.Address.Id.ShouldNotBe(Guid.Empty);
        subject.Address.Street.ShouldBe("Unter den Linden 77");
        subject.Address.City.ShouldBe("Berlin");
        subject.Address.PostalCode.ShouldBe("10117");
        subject.Address.Country.Id.ShouldBe(TestData.CzCountryId);
        subject.Address.Country.Code.ShouldBe("CZ");
        subject.Address.Country.Name.ShouldBe("Česká republika");
    }
    
    [Fact]
    public async Task Should_Throw_CreateWithAddress_With_Invalid_Address()
    {
        var id = _guidGenerator.Create();
        var addressInput = Converter.CreateAddressInput(id, postalCode: RandomGenerator.RandomAlpNum(FakeAddress.MaxPostalCodeLength + 10));
        var input = Converter.CreateSubjectWithAddressInput(addressInput);
        
        var exception = await Should.ThrowAsync<AbpValidationException>(async () =>
        {
            await _subjectAppService.CreateWithAddressAsync(input);
        });

        var addresses = await _addressAppService.GetAllAsync(new PagedAndSortedResultRequestDto());
        
        exception.ValidationErrors.First().ErrorMessage!.ShouldContain(nameof(Entities.Address.PostalCode));
        addresses.Items.Count.ShouldBe(3);
    }
    
    [Fact]
    public async Task Should_Throw_CreateWithAddress_With_Invalid_Subject()
    {
        var addressInput = Converter.CreateAddressInput(TestData.CzCountryId);
        var input = Converter.CreateSubjectWithAddressInput(addressInput, ic: RandomGenerator.RandomAlpNum(FakeSubject.MaxIcLength + 10));
        
        var exception = await Should.ThrowAsync<ArgumentException>(async () =>
        {
            await _subjectAppService.CreateWithAddressAsync(input);
        });
        var addresses = await _addressAppService.GetAllAsync(new PagedAndSortedResultRequestDto());
        
        exception.Message.ShouldContain(nameof(Entities.Subject.Ic));
        addresses.Items.Count.ShouldBe(3);
    }
    
    [Fact]
    public async Task Should_Throw_CreateWithAddress_With_Non_Existing_Country()
    {
        var id = _guidGenerator.Create();
        var addressInput = Converter.CreateAddressInput(id);
        var input = Converter.CreateSubjectWithAddressInput(addressInput);
        
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _subjectAppService.CreateWithAddressAsync(input);
        });

        var addresses = await _addressAppService.GetAllAsync(new PagedAndSortedResultRequestDto());
        
        exception.Message.ShouldContain(nameof(Entities.Country));
        exception.Message.ShouldContain(id.ToString());
        addresses.Items.Count.ShouldBe(3);
    }
    
    [Fact]
    public async Task Should_UpdateWithAddress()
    {
        var subject = await _subjectAppService.GetAsync(TestData.CzSubjectId);
        subject.Name = Converter.DefaultSubjectName;
        subject.Ic = Converter.DefaultSubjectIc;
        subject.Dic = Converter.DefaultSubjectDic;
        subject.IsVatPayer = Converter.DefaultSubjectIsVatPayer;
        subject.Address.Id = TestData.UsAddressId;
        subject.Address.Street = "1600 Pennsylvania Avenue NW";
        subject.Address.City = "Washington";
        subject.Address.PostalCode = Converter.DefaultAddressPostalCode;
        
        var updatedSubject = await _subjectAppService.UpdateWithAddressAsync(Converter.Convert2UpdateInput(subject, true));
        
        updatedSubject.Id.ShouldBe(subject.Id);
        updatedSubject.Name.ShouldBe(subject.Name);
        updatedSubject.Ic.ShouldBe(subject.Ic);
        updatedSubject.Dic.ShouldBe(subject.Dic);
        updatedSubject.IsVatPayer.ShouldBe(subject.IsVatPayer);
        updatedSubject.Address.Id.ShouldBe(TestData.UsAddressId);
        updatedSubject.Address.Street.ShouldBe("1600 Pennsylvania Avenue NW");
        updatedSubject.Address.City.ShouldBe("Washington");
        updatedSubject.Address.PostalCode.ShouldBe(Converter.DefaultAddressPostalCode);
        updatedSubject.Address.Country.Id.ShouldBe(TestData.CzCountryId);
        updatedSubject.Address.Country.Code.ShouldBe("CZ");
        updatedSubject.Address.Country.Name.ShouldBe("Česká republika");
    }
    
    [Fact]
    public async Task Should_UpdateWithAddress_Without_Address()
    {
        var id = _guidGenerator.Create();
        var subject = await _subjectAppService.GetAsync(TestData.CzSubjectId);
        subject.Address.Id = id;
        subject.Name = Converter.DefaultSubjectName;
        subject.Ic = Converter.DefaultSubjectIc;
        subject.Dic = Converter.DefaultSubjectDic;
        subject.IsVatPayer = Converter.DefaultSubjectIsVatPayer;
        
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _subjectAppService.UpdateWithAddressAsync(Converter.Convert2UpdateInput(subject, true));
        });
        var addresses = await _addressAppService.GetAllAsync(new PagedAndSortedResultRequestDto());
        
        exception.Message.ShouldContain(nameof(Entities.Address));
        exception.Message.ShouldContain(id.ToString());
        addresses.Items.Count.ShouldBe(3);
    }
    
    [Fact]
    public async Task Should_UpdateWithAddress_Without_Subject()
    {
        var id = _guidGenerator.Create();
        var subject = await _subjectAppService.GetAsync(TestData.CzSubjectId);
        subject.Id = id;
        subject.Name = Converter.DefaultSubjectName;
        subject.Ic = Converter.DefaultSubjectIc;
        subject.Dic = Converter.DefaultSubjectDic;
        subject.IsVatPayer = Converter.DefaultSubjectIsVatPayer;
        
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _subjectAppService.UpdateWithAddressAsync(Converter.Convert2UpdateInput(subject, true));
        });
        var addresses = await _addressAppService.GetAllAsync(new PagedAndSortedResultRequestDto());
        
        exception.Message.ShouldContain(nameof(Entities.Subject));
        exception.Message.ShouldContain(id.ToString());
        addresses.Items.Count.ShouldBe(3);
    }
    
    [Fact]
    public async Task Should_Throw_UpdateWithAddress_With_Invalid_Address()
    {
        var subject = await _subjectAppService.GetAsync(TestData.CzSubjectId);
        subject.Address.PostalCode = RandomGenerator.RandomAlpNum(FakeAddress.MaxPostalCodeLength + 10);
        subject.Name = Converter.DefaultSubjectName;
        subject.Ic = Converter.DefaultSubjectIc;
        subject.Dic = Converter.DefaultSubjectDic;
        subject.IsVatPayer = Converter.DefaultSubjectIsVatPayer;
        
        var exception = await Should.ThrowAsync<AbpValidationException>(async () =>
        {
            await _subjectAppService.UpdateWithAddressAsync(Converter.Convert2UpdateInput(subject, true));
        });
        var addresses = await _addressAppService.GetAllAsync(new PagedAndSortedResultRequestDto());
        
        exception.ValidationErrors.First().ErrorMessage!.ShouldContain(nameof(Entities.Address.PostalCode));
        addresses.Items.Count.ShouldBe(3);
    }
    
    [Fact]
    public async Task Should_Throw_UpdateWithAddress_With_Invalid_Subject()
    {
        var subject = await _subjectAppService.GetAsync(TestData.CzSubjectId);
        subject.Name = Converter.DefaultSubjectName;
        subject.Ic = RandomGenerator.RandomAlpNum(FakeSubject.MaxIcLength + 10);
        subject.Dic = Converter.DefaultSubjectDic;
        subject.IsVatPayer = Converter.DefaultSubjectIsVatPayer;
        
        var exception = await Should.ThrowAsync<ArgumentException>(async () =>
        {
            await _subjectAppService.UpdateWithAddressAsync(Converter.Convert2UpdateInput(subject, true));
        });
        var addresses = await _addressAppService.GetAllAsync(new PagedAndSortedResultRequestDto());
        
        exception.Message.ShouldContain(nameof(Entities.Subject.Ic));
        addresses.Items.Count.ShouldBe(3);
    }
    
    [Fact]
    public async Task Should_Throw_UpdateWithAddress_With_Non_Existing_Country()
    {
        var id = _guidGenerator.Create();
        var subject = await _subjectAppService.GetAsync(TestData.CzSubjectId);
        subject.Name = Converter.DefaultSubjectName;
        subject.Dic = Converter.DefaultSubjectDic;
        subject.IsVatPayer = Converter.DefaultSubjectIsVatPayer;
        subject.Address.Country.Id = id;
        
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _subjectAppService.UpdateWithAddressAsync(Converter.Convert2UpdateInput(subject, true));
        });
        var addresses = await _addressAppService.GetAllAsync(new PagedAndSortedResultRequestDto());
        
        exception.Message.ShouldContain(nameof(Entities.Country));
        addresses.Items.Count.ShouldBe(3);
    }
}