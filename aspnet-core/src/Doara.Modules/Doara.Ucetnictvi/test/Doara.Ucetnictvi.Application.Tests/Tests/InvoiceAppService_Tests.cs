using System;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Enums;
using Doara.Ucetnictvi.IAppServices;
using Doara.Ucetnictvi.Utils.Converters;
using Shouldly;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Xunit;

namespace Doara.Ucetnictvi.Tests;

public class InvoiceAppService_Tests : UcetnictviApplicationTestBase<UcetnictviApplicationTestModule>
{
    private readonly IInvoiceAppService _invoiceAppService;
    private readonly IGuidGenerator _guidGenerator;
    
    public InvoiceAppService_Tests()
    {
        _invoiceAppService = GetRequiredService<IInvoiceAppService>();
        _guidGenerator = GetRequiredService<IGuidGenerator>();
    }
    
    [Fact]
    public async Task Should_Get_Invoice()
    {
        var czInvoice = await _invoiceAppService.GetAsync(TestData.CzInvoiceId);
        czInvoice.Id.ShouldBe(TestData.CzInvoiceId);
        czInvoice.InvoiceNumber.ShouldBe("2025-1001");
        czInvoice.IssueDate.ShouldBe(new DateTime(2025, 4, 10));
        czInvoice.TaxDate.ShouldBe(new DateTime(2025, 4, 10));
        czInvoice.DeliveryDate.ShouldBe(new DateTime(2025, 4, 9));
        czInvoice.TotalNetAmount.ShouldBe(10000m);
        czInvoice.TotalVatAmount.ShouldBe(2100m);
        czInvoice.TotalGrossAmount.ShouldBe(12100m);
        czInvoice.PaymentTerms.ShouldBe("Splatnost do 14 dnů");
        czInvoice.VatRate.ShouldBe(VatRate.Standard21);
        czInvoice.VariableSymbol.ShouldBe("10012025");
        czInvoice.ConstantSymbol.ShouldBe("0308");
        czInvoice.SpecificSymbol.ShouldBe("0555");
        
        czInvoice.Supplier.Id.ShouldBe(TestData.CzSubjectId);
        czInvoice.Supplier.Name.ShouldBe("Alza.cz a.s.");
        czInvoice.Supplier.Ic.ShouldBe("27082440");
        czInvoice.Supplier.Dic.ShouldBe("CZ27082440");
        czInvoice.Supplier.IsVatPayer.ShouldBeTrue();
        czInvoice.Supplier.Address.Id.ShouldBe(TestData.CzAddressId);
        czInvoice.Supplier.Address.Street.ShouldBe("Václavské náměstí 1");
        czInvoice.Supplier.Address.City.ShouldBe("Praha");
        czInvoice.Supplier.Address.PostalCode.ShouldBe("11000");
        czInvoice.Supplier.Address.Country.Id.ShouldBe(TestData.CzCountryId);
        czInvoice.Supplier.Address.Country.Code.ShouldBe("CZ");
        czInvoice.Supplier.Address.Country.Name.ShouldBe("Česká republika");
        
        czInvoice.Customer.Id.ShouldBe(TestData.SkSubjectId);
        czInvoice.Customer.Name.ShouldBe("Martinus, s.r.o.");
        czInvoice.Customer.Ic.ShouldBe("35840773");
        czInvoice.Customer.Dic.ShouldBe("SK2020269786");
        czInvoice.Customer.IsVatPayer.ShouldBeTrue();
        czInvoice.Customer.Address.Id.ShouldBe(TestData.SkAddressId);
        czInvoice.Customer.Address.Street.ShouldBe("Hlavná 5");
        czInvoice.Customer.Address.City.ShouldBe("Bratislava");
        czInvoice.Customer.Address.PostalCode.ShouldBe("81101");
        czInvoice.Customer.Address.Country.Id.ShouldBe(TestData.SkCountryId);
        czInvoice.Customer.Address.Country.Code.ShouldBe("SK");
        czInvoice.Customer.Address.Country.Name.ShouldBe("Slovensko");
        
        czInvoice.Items.Count.ShouldBe(1);
        czInvoice.Items[0].Id.ShouldBe(TestData.CzInvoiceItemId);
    }
    
    [Fact]
    public async Task Should_Throw_Get_Non_Existing_Invoice()
    {
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _invoiceAppService.GetAsync(id);
        });
        exception.Message.ShouldContain(nameof(Entities.Invoice));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Create_Invoice()
    {
        var input = Converter.CreateInvoiceInput(TestData.CzSubjectId, TestData.SkSubjectId, Converter.DefaultInvoiceIssueDate,
            Converter.DefaultInvoiceTaxDate, Converter.DefaultInvoiceDeliveryDate);
        var invoice = await _invoiceAppService.CreateAsync(input);
        
        invoice.Id.ShouldNotBe(Guid.Empty);
        
        invoice.InvoiceNumber.ShouldBe(input.InvoiceNumber);
        invoice.IssueDate.ShouldBe(input.IssueDate);
        invoice.TaxDate.ShouldBe(input.TaxDate);
        invoice.DeliveryDate.ShouldBe(input.DeliveryDate);
        invoice.TotalNetAmount.ShouldBe(input.TotalNetAmount);
        invoice.TotalVatAmount.ShouldBe(input.TotalVatAmount);
        invoice.TotalGrossAmount.ShouldBe(input.TotalGrossAmount);
        invoice.PaymentTerms.ShouldBe(input.PaymentTerms);
        invoice.VatRate.ShouldBe(input.VatRate);
        invoice.VariableSymbol.ShouldBe(input.VariableSymbol);
        invoice.ConstantSymbol.ShouldBe(input.ConstantSymbol);
        invoice.SpecificSymbol.ShouldBe(input.SpecificSymbol);
        
        invoice.Supplier.Id.ShouldBe(TestData.CzSubjectId);
        invoice.Supplier.Name.ShouldBe("Alza.cz a.s.");
        invoice.Supplier.Ic.ShouldBe("27082440");
        invoice.Supplier.Dic.ShouldBe("CZ27082440");
        invoice.Supplier.IsVatPayer.ShouldBeTrue();
        invoice.Supplier.Address.Id.ShouldBe(TestData.CzAddressId);
        invoice.Supplier.Address.Street.ShouldBe("Václavské náměstí 1");
        invoice.Supplier.Address.City.ShouldBe("Praha");
        invoice.Supplier.Address.PostalCode.ShouldBe("11000");
        invoice.Supplier.Address.Country.Id.ShouldBe(TestData.CzCountryId);
        invoice.Supplier.Address.Country.Code.ShouldBe("CZ");
        invoice.Supplier.Address.Country.Name.ShouldBe("Česká republika");
        
        invoice.Customer.Id.ShouldBe(TestData.SkSubjectId);
        invoice.Customer.Name.ShouldBe("Martinus, s.r.o.");
        invoice.Customer.Ic.ShouldBe("35840773");
        invoice.Customer.Dic.ShouldBe("SK2020269786");
        invoice.Customer.IsVatPayer.ShouldBeTrue();
        invoice.Customer.Address.Id.ShouldBe(TestData.SkAddressId);
        invoice.Customer.Address.Street.ShouldBe("Hlavná 5");
        invoice.Customer.Address.City.ShouldBe("Bratislava");
        invoice.Customer.Address.PostalCode.ShouldBe("81101");
        invoice.Customer.Address.Country.Id.ShouldBe(TestData.SkCountryId);
        invoice.Customer.Address.Country.Code.ShouldBe("SK");
        invoice.Customer.Address.Country.Name.ShouldBe("Slovensko");
        
        invoice.Items.Count.ShouldBe(0);
    }
    
    [Fact]
    public async Task Should_Throw_Create_Invoice_Non_Existing_Supplier()
    {
        var id = _guidGenerator.Create();
        var input = Converter.CreateInvoiceInput(id, TestData.SkSubjectId, Converter.DefaultInvoiceIssueDate,
            Converter.DefaultInvoiceTaxDate, Converter.DefaultInvoiceDeliveryDate);
        
        var exception = await Should.ThrowAsync<BusinessException>(async () =>
        {
            await _invoiceAppService.CreateAsync(input);
        });
        
        exception.Code.ShouldBe(UcetnictviErrorCodes.SupplierDoesNotExist);
    }
    
    [Fact]
    public async Task Should_Throw_Create_Invoice_Non_Existing_Customer()
    {
        var id = _guidGenerator.Create();
        var input = Converter.CreateInvoiceInput(TestData.SkSubjectId, id, Converter.DefaultInvoiceIssueDate,
            Converter.DefaultInvoiceTaxDate, Converter.DefaultInvoiceDeliveryDate);
        
        var exception = await Should.ThrowAsync<BusinessException>(async () =>
        {
            await _invoiceAppService.CreateAsync(input);
        });
        
        exception.Code.ShouldBe(UcetnictviErrorCodes.CustomerDoesNotExist);
    }
    
    [Fact]
    public async Task Should_Throw_Create_Invoice_With_Same_Customer_And_Supplier()
    {
        var input = Converter.CreateInvoiceInput(TestData.SkSubjectId, TestData.SkSubjectId, Converter.DefaultInvoiceIssueDate,
            Converter.DefaultInvoiceTaxDate, Converter.DefaultInvoiceDeliveryDate);
        
        var exception = await Should.ThrowAsync<BusinessException>(async () =>
        {
            await _invoiceAppService.CreateAsync(input);
        });
        
        exception.Code.ShouldBe(UcetnictviErrorCodes.SupplierIsSameAsCustomer);
    }
    
    [Fact]
    public async Task Should_Update_Invoice()
    {
        var invoice = await _invoiceAppService.GetAsync(TestData.CzInvoiceId);
        invoice.InvoiceNumber = Converter.DefaultInvoiceInvoiceNumber;
        invoice.Customer.Id = TestData.CzSubjectId;
        invoice.Supplier.Id = TestData.SkSubjectId;
        invoice.IssueDate = Converter.DefaultInvoiceIssueDate;
        invoice.TaxDate = Converter.DefaultInvoiceTaxDate;
        invoice.DeliveryDate = Converter.DefaultInvoiceDeliveryDate;
        invoice.TotalNetAmount = Converter.DefaultInvoiceTotalNetAmount;
        invoice.TotalVatAmount = Converter.DefaultInvoiceTotalVatAmount;
        invoice.TotalGrossAmount = Converter.DefaultInvoiceTotalGrossAmount;
        invoice.PaymentTerms = Converter.DefaultInvoicePaymentTerms;
        invoice.VatRate = Converter.DefaultInvoiceVatRate;
        invoice.VariableSymbol = Converter.DefaultInvoiceVariableSymbol;
        invoice.ConstantSymbol = Converter.DefaultInvoiceConstantSymbol;
        invoice.SpecificSymbol = Converter.DefaultInvoiceSpecificSymbol;
        var input = Converter.Convert2UpdateInput(invoice);
        var updatedInvoice = await _invoiceAppService.UpdateAsync(input);
        
        updatedInvoice.Id.ShouldBe(input.Id);
        updatedInvoice.InvoiceNumber.ShouldBe(input.InvoiceNumber);
        updatedInvoice.IssueDate.ShouldBe(input.IssueDate);
        updatedInvoice.TaxDate.ShouldBe(input.TaxDate);
        updatedInvoice.DeliveryDate.ShouldBe(input.DeliveryDate);
        updatedInvoice.TotalNetAmount.ShouldBe(input.TotalNetAmount);
        updatedInvoice.TotalVatAmount.ShouldBe(input.TotalVatAmount);
        updatedInvoice.TotalGrossAmount.ShouldBe(input.TotalGrossAmount);
        updatedInvoice.PaymentTerms.ShouldBe(input.PaymentTerms);
        updatedInvoice.VatRate.ShouldBe(input.VatRate);
        updatedInvoice.VariableSymbol.ShouldBe(input.VariableSymbol);
        updatedInvoice.ConstantSymbol.ShouldBe(input.ConstantSymbol);
        updatedInvoice.SpecificSymbol.ShouldBe(input.SpecificSymbol);
        
        updatedInvoice.Customer.Id.ShouldBe(TestData.CzSubjectId);
        updatedInvoice.Customer.Name.ShouldBe("Alza.cz a.s.");
        updatedInvoice.Customer.Ic.ShouldBe("27082440");
        updatedInvoice.Customer.Dic.ShouldBe("CZ27082440");
        updatedInvoice.Customer.IsVatPayer.ShouldBeTrue();
        updatedInvoice.Customer.Address.Id.ShouldBe(TestData.CzAddressId);
        updatedInvoice.Customer.Address.Street.ShouldBe("Václavské náměstí 1");
        updatedInvoice.Customer.Address.City.ShouldBe("Praha");
        updatedInvoice.Customer.Address.PostalCode.ShouldBe("11000");
        updatedInvoice.Customer.Address.Country.Id.ShouldBe(TestData.CzCountryId);
        updatedInvoice.Customer.Address.Country.Code.ShouldBe("CZ");
        updatedInvoice.Customer.Address.Country.Name.ShouldBe("Česká republika");
        
        updatedInvoice.Supplier.Id.ShouldBe(TestData.SkSubjectId);
        updatedInvoice.Supplier.Name.ShouldBe("Martinus, s.r.o.");
        updatedInvoice.Supplier.Ic.ShouldBe("35840773");
        updatedInvoice.Supplier.Dic.ShouldBe("SK2020269786");
        updatedInvoice.Supplier.IsVatPayer.ShouldBeTrue();
        updatedInvoice.Supplier.Address.Id.ShouldBe(TestData.SkAddressId);
        updatedInvoice.Supplier.Address.Street.ShouldBe("Hlavná 5");
        updatedInvoice.Supplier.Address.City.ShouldBe("Bratislava");
        updatedInvoice.Supplier.Address.PostalCode.ShouldBe("81101");
        updatedInvoice.Supplier.Address.Country.Id.ShouldBe(TestData.SkCountryId);
        updatedInvoice.Supplier.Address.Country.Code.ShouldBe("SK");
        updatedInvoice.Supplier.Address.Country.Name.ShouldBe("Slovensko");
        
        updatedInvoice.Items.Count.ShouldBe(1);
        updatedInvoice.Items[0].Id.ShouldBe(TestData.CzInvoiceItemId);
    }
    
    [Fact]
    public async Task Should_Throw_Update_Non_Existing_Invoice()
    {
        var id = _guidGenerator.Create();
        var invoice = await _invoiceAppService.GetAsync(TestData.CzInvoiceId);
        invoice.InvoiceNumber = Converter.DefaultInvoiceInvoiceNumber;
        invoice.Customer.Id = TestData.CzSubjectId;
        invoice.Supplier.Id = TestData.SkSubjectId;
        invoice.IssueDate = Converter.DefaultInvoiceIssueDate;
        invoice.TaxDate = Converter.DefaultInvoiceTaxDate;
        invoice.DeliveryDate = Converter.DefaultInvoiceDeliveryDate;
        invoice.TotalNetAmount = Converter.DefaultInvoiceTotalNetAmount;
        invoice.TotalVatAmount = Converter.DefaultInvoiceTotalVatAmount;
        invoice.TotalGrossAmount = Converter.DefaultInvoiceTotalGrossAmount;
        invoice.PaymentTerms = Converter.DefaultInvoicePaymentTerms;
        invoice.VatRate = Converter.DefaultInvoiceVatRate;
        invoice.VariableSymbol = Converter.DefaultInvoiceVariableSymbol;
        invoice.ConstantSymbol = Converter.DefaultInvoiceConstantSymbol;
        invoice.SpecificSymbol = Converter.DefaultInvoiceSpecificSymbol;
        invoice.Id = id;

        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _invoiceAppService.UpdateAsync(Converter.Convert2UpdateInput(invoice));
        });
        exception.Message.ShouldContain(nameof(Entities.Invoice));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Throw_Update_Invoice_Non_Existing_Supplier()
    {
        var id = _guidGenerator.Create();
        var invoice = await _invoiceAppService.GetAsync(TestData.CzInvoiceId);
        invoice.InvoiceNumber = Converter.DefaultInvoiceInvoiceNumber;
        invoice.Customer.Id = TestData.CzSubjectId;
        invoice.Supplier.Id = id;
        invoice.IssueDate = Converter.DefaultInvoiceIssueDate;
        invoice.TaxDate = Converter.DefaultInvoiceTaxDate;
        invoice.DeliveryDate = Converter.DefaultInvoiceDeliveryDate;
        invoice.TotalNetAmount = Converter.DefaultInvoiceTotalNetAmount;
        invoice.TotalVatAmount = Converter.DefaultInvoiceTotalVatAmount;
        invoice.TotalGrossAmount = Converter.DefaultInvoiceTotalGrossAmount;
        invoice.PaymentTerms = Converter.DefaultInvoicePaymentTerms;
        invoice.VatRate = Converter.DefaultInvoiceVatRate;
        invoice.VariableSymbol = Converter.DefaultInvoiceVariableSymbol;
        invoice.ConstantSymbol = Converter.DefaultInvoiceConstantSymbol;
        invoice.SpecificSymbol = Converter.DefaultInvoiceSpecificSymbol;

        var exception = await Should.ThrowAsync<BusinessException>(async () =>
        {
            await _invoiceAppService.UpdateAsync(Converter.Convert2UpdateInput(invoice));
        });
        exception.Code.ShouldBe(UcetnictviErrorCodes.SupplierDoesNotExist);
    }
    
    [Fact]
    public async Task Should_Throw_Update_Invoice_Non_Existing_Customer()
    {
        var id = _guidGenerator.Create();
        var invoice = await _invoiceAppService.GetAsync(TestData.CzInvoiceId);
        invoice.InvoiceNumber = Converter.DefaultInvoiceInvoiceNumber;
        invoice.Customer.Id = id;
        invoice.Supplier.Id = TestData.SkSubjectId;
        invoice.IssueDate = Converter.DefaultInvoiceIssueDate;
        invoice.TaxDate = Converter.DefaultInvoiceTaxDate;
        invoice.DeliveryDate = Converter.DefaultInvoiceDeliveryDate;
        invoice.TotalNetAmount = Converter.DefaultInvoiceTotalNetAmount;
        invoice.TotalVatAmount = Converter.DefaultInvoiceTotalVatAmount;
        invoice.TotalGrossAmount = Converter.DefaultInvoiceTotalGrossAmount;
        invoice.PaymentTerms = Converter.DefaultInvoicePaymentTerms;
        invoice.VatRate = Converter.DefaultInvoiceVatRate;
        invoice.VariableSymbol = Converter.DefaultInvoiceVariableSymbol;
        invoice.ConstantSymbol = Converter.DefaultInvoiceConstantSymbol;
        invoice.SpecificSymbol = Converter.DefaultInvoiceSpecificSymbol;
        
        var exception = await Should.ThrowAsync<BusinessException>(async () =>
        {
            await _invoiceAppService.UpdateAsync(Converter.Convert2UpdateInput(invoice));
        });
        
        exception.Code.ShouldBe(UcetnictviErrorCodes.CustomerDoesNotExist);
    }
    
    [Fact]
    public async Task Should_Throw_Update_Invoice_With_Same_Customer_And_Supplier()
    {
        var invoice = await _invoiceAppService.GetAsync(TestData.CzInvoiceId);
        invoice.InvoiceNumber = Converter.DefaultInvoiceInvoiceNumber;
        invoice.Customer.Id = TestData.SkSubjectId;
        invoice.Supplier.Id = TestData.SkSubjectId;
        invoice.IssueDate = Converter.DefaultInvoiceIssueDate;
        invoice.TaxDate = Converter.DefaultInvoiceTaxDate;
        invoice.DeliveryDate = Converter.DefaultInvoiceDeliveryDate;
        invoice.TotalNetAmount = Converter.DefaultInvoiceTotalNetAmount;
        invoice.TotalVatAmount = Converter.DefaultInvoiceTotalVatAmount;
        invoice.TotalGrossAmount = Converter.DefaultInvoiceTotalGrossAmount;
        invoice.PaymentTerms = Converter.DefaultInvoicePaymentTerms;
        invoice.VatRate = Converter.DefaultInvoiceVatRate;
        invoice.VariableSymbol = Converter.DefaultInvoiceVariableSymbol;
        invoice.ConstantSymbol = Converter.DefaultInvoiceConstantSymbol;
        invoice.SpecificSymbol = Converter.DefaultInvoiceSpecificSymbol;
        
        var exception = await Should.ThrowAsync<BusinessException>(async () =>
        {
            await _invoiceAppService.UpdateAsync(Converter.Convert2UpdateInput(invoice));
        });
        
        exception.Code.ShouldBe(UcetnictviErrorCodes.SupplierIsSameAsCustomer);
    }
    
    [Fact]
    public async Task Should_Delete_Invoice()
    {
        await _invoiceAppService.DeleteAsync(TestData.CzInvoiceId);

        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _invoiceAppService.GetAsync(TestData.CzInvoiceId);
        });
        exception.Message.ShouldContain(nameof(Entities.Invoice));
        exception.Message.ShouldContain(TestData.CzInvoiceId.ToString());
    }

    [Fact]
    public async Task Should_Throw_Delete_Non_Existing_Invoice()
    {
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _invoiceAppService.DeleteAsync(id);
        });
        exception.Message.ShouldContain(nameof(Entities.Invoice));
        exception.Message.ShouldContain(id.ToString());
    }

    [Fact]
    public async Task Should_GetAll()
    {
        var entities = await _invoiceAppService.GetAllAsync(new PagedAndSortedResultRequestDto
        {
            SkipCount = 1,
            MaxResultCount = 2,
            Sorting = "InvoiceNumber desc"
        });

        entities.TotalCount.ShouldBe(4);
        entities.Items.Count.ShouldBe(2);
        entities.Items[0].Id.ShouldBe(TestData.SkInvoiceId2);
        entities.Items[1].Id.ShouldBe(TestData.SkInvoiceId1);
    }
}