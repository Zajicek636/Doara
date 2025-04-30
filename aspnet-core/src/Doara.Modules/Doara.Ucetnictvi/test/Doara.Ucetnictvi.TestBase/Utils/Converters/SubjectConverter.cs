using System;
using Doara.Ucetnictvi.Dto.Address;
using Doara.Ucetnictvi.Dto.Subject;

namespace Doara.Ucetnictvi.Utils.Converters;

public static partial class Converter
{
    public const string DefaultSubjectName = "Deutsche Telekom AG";
    public const string DefaultSubjectIc = "13265272";
    public const string DefaultSubjectDic = "DE811858158";
    public const bool DefaultSubjectIsVatPayer = true;
    
    public static SubjectCreateInputDto CreateSubjectInput(Guid addressId, string name = DefaultSubjectName, string? ic = DefaultSubjectIc,
        string? dic = DefaultSubjectDic, bool isVatPayer = DefaultSubjectIsVatPayer)
    {
        return new SubjectCreateInputDto
        {
            Name = name,
            Ic = ic,
            Dic = dic,
            IsVatPayer = isVatPayer,
            AddressId = addressId
        };
    }
    
    public static SubjectWithAddressCreateInputDto CreateSubjectWithAddressInput(AddressCreateInputDto addressInput, string name = DefaultSubjectName, string? ic = DefaultSubjectIc,
        string? dic = DefaultSubjectDic, bool isVatPayer = DefaultSubjectIsVatPayer)
    {
        return new SubjectWithAddressCreateInputDto
        {
            Name = name,
            Ic = ic,
            Dic = dic,
            IsVatPayer = isVatPayer,
            Address = addressInput
        };
    }
    
    public static SubjectUpdateInputDto Convert2UpdateInput(SubjectDetailDto input)
    {
        return new SubjectUpdateInputDto
        {
            Name = input.Name,
            Ic = input.Ic,
            Dic = input.Dic,
            IsVatPayer = input.IsVatPayer,
            AddressId = input.Address.Id
        };
    }
    
    public static SubjectWithAddressUpdateInputDto Convert2UpdateInput(SubjectDetailDto input, bool setAddress)
    {
        return new SubjectWithAddressUpdateInputDto
        {
            Name = input.Name,
            Ic = input.Ic,
            Dic = input.Dic,
            IsVatPayer = input.IsVatPayer,
            Address = setAddress ? Convert2UpdateInput(input.Address) : null!
        };
    }
}