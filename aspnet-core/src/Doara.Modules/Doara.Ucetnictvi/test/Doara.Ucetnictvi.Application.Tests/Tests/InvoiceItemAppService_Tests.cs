using System;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Enums;
using Doara.Ucetnictvi.IAppServices;
using Doara.Ucetnictvi.Utils.Converters;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Xunit;

namespace Doara.Ucetnictvi.Tests;

public class InvoiceItemAppService_Tests : UcetnictviApplicationTestBase<UcetnictviApplicationTestModule>
{
    private readonly IInvoiceItemAppService _invoiceItemAppService;
    private readonly IGuidGenerator _guidGenerator;
    
    public InvoiceItemAppService_Tests()
    {
        _invoiceItemAppService = GetRequiredService<IInvoiceItemAppService>();
        _guidGenerator = GetRequiredService<IGuidGenerator>();
    }

    [Fact]
    public async Task Should_Get_InvoiceItem()
    {
        var czInvoiceItem = await _invoiceItemAppService.GetAsync(TestData.CzInvoiceItemId);
        czInvoiceItem.Id.ShouldBe(TestData.CzInvoiceItemId);
        czInvoiceItem.InvoiceId.ShouldBe(TestData.CzInvoiceId);
        czInvoiceItem.Description.ShouldBe("Dodávka IT služeb");
        czInvoiceItem.Quantity.ShouldBe(1);
        czInvoiceItem.UnitPrice.ShouldBe(10000m);
        czInvoiceItem.NetAmount.ShouldBe(10000m);
        czInvoiceItem.VatRate.ShouldBe(VatRate.Standard21);
        czInvoiceItem.VatAmount.ShouldBe(2100m);
        czInvoiceItem.GrossAmount.ShouldBe(12100m);
    }
    
    [Fact]
    public async Task Should_Throw_Get_Non_Existing_InvoiceItem()
    {
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _invoiceItemAppService.GetAsync(id);
        });
        exception.Message.ShouldContain(nameof(Entities.InvoiceItem));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_GetAll()
    {
        var entities = await _invoiceItemAppService.GetAllAsync(new PagedAndSortedResultRequestDto
        {
            SkipCount = 4,
            MaxResultCount = 10,
            Sorting = "Description desc"
        });

        entities.TotalCount.ShouldBe(5);
        entities.Items.Count.ShouldBe(1);
        
        entities.Items[0].Id.ShouldBe(TestData.CzInvoiceItemId);
    }
    
    [Fact]
    public async Task Should_ManageMany_InvoiceItem()
    {
        /*var input = Converter.CreateInvoiceItemInput(TestData.CzInvoiceItemId);
        var invoiceItem = await _invoiceItemAppService.CreateAsync(input);

        invoiceItem.Id.ShouldNotBe(Guid.Empty);
        invoiceItem.InvoiceId.ShouldBe(input.InvoiceId);
        invoiceItem.Description.ShouldBe(input.Description);
        invoiceItem.Quantity.ShouldBe(input.Quantity);
        invoiceItem.UnitPrice.ShouldBe(input.UnitPrice);
        invoiceItem.NetAmount.ShouldBe(input.NetAmount);
        invoiceItem.VatRate.ShouldBe(input.VatRate ?? VatRate.None);
        invoiceItem.VatAmount.ShouldBe(input.VatAmount);
        invoiceItem.GrossAmount.ShouldBe(input.GrossAmount);*/
    }
}