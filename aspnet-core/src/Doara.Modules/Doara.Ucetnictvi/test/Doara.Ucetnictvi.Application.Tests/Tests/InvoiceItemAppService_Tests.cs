using System;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.InvoiceItem;
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
        var entities = await _invoiceItemAppService.GetAllAsync(new InvoiceItemGetAllDto
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
    public async Task Should_GetAll_With_InvoiceId()
    {
        var entities = await _invoiceItemAppService.GetAllAsync(new InvoiceItemGetAllDto
        {
            InvoiceId = TestData.SkInvoiceId3,
            SkipCount = 2,
            MaxResultCount = 10,
            Sorting = "Description desc"
        });

        entities.TotalCount.ShouldBe(3);
        entities.Items.Count.ShouldBe(1);
        
        entities.Items[0].Id.ShouldBe(TestData.SkInvoiceItemId33);
    }

    [Fact]
    public async Task Should_ManageMany_InvoiceItem()
    {
        var input = new InvoiceItemManageManyInputDto
        {
            InvoiceId = TestData.SkInvoiceId3,
            Items =
            [
                Converter.CreateInvoiceItemManageMany(TestData.SkInvoiceItemId31,
                    description: "Update"),
                Converter.CreateInvoiceItemManageMany(description: "Create")
            ],
            ItemsForDelete = [TestData.SkInvoiceItemId32],
            DeleteMissingItems = false
        };

        var report = await _invoiceItemAppService.ManageManyAsync(input);
        var items = await _invoiceItemAppService.GetAllAsync(new InvoiceItemGetAllDto
        {
            InvoiceId = TestData.SkInvoiceId3,
            Sorting = "Description"
        });
        
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _invoiceItemAppService.GetAsync(TestData.SkInvoiceItemId32);
        });
        exception.Message.ShouldContain(nameof(Entities.InvoiceItem));
        exception.Message.ShouldContain(TestData.SkInvoiceItemId32.ToString());

        items.TotalCount.ShouldBe(3);
        items.Items.Count.ShouldBe(3);
        items.Items[0].Id.ShouldNotBe(TestData.SkInvoiceItemId31);
        items.Items[0].Id.ShouldNotBe(TestData.SkInvoiceItemId32);
        items.Items[0].Id.ShouldNotBe(TestData.SkInvoiceItemId33);
        items.Items[0].Description.ShouldBe("Create");
        
        items.Items[1].Id.ShouldBe(TestData.SkInvoiceItemId33);
        items.Items[1].Description.ShouldBe("Dokumentácia");
        
        items.Items[2].Id.ShouldBe(TestData.SkInvoiceItemId31);
        items.Items[2].Description.ShouldBe("Update");

        report.HasErrors.ShouldBeFalse();
        report.Errors.Count.ShouldBe(0);
    }
    
    [Fact]
    public async Task Should_ManageMany_InvoiceItem_With_Delete_Missing()
    {
        var input = new InvoiceItemManageManyInputDto
        {
            InvoiceId = TestData.SkInvoiceId3,
            Items =
            [
                Converter.CreateInvoiceItemManageMany(TestData.SkInvoiceItemId31,
                    description: "Update"),
                Converter.CreateInvoiceItemManageMany(description: "Create")
            ],
            ItemsForDelete = [TestData.SkInvoiceItemId31],
            DeleteMissingItems = true
        };

        var report = await _invoiceItemAppService.ManageManyAsync(input);
        var items = await _invoiceItemAppService.GetAllAsync(new InvoiceItemGetAllDto
        {
            InvoiceId = TestData.SkInvoiceId3,
            Sorting = "Description"
        });
        
        var exception1 = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _invoiceItemAppService.GetAsync(TestData.SkInvoiceItemId31);
        });
        var exception2 = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _invoiceItemAppService.GetAsync(TestData.SkInvoiceItemId32);
        });
        var exception3 = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _invoiceItemAppService.GetAsync(TestData.SkInvoiceItemId33);
        });
        
        exception1.Message.ShouldContain(nameof(Entities.InvoiceItem));
        exception1.Message.ShouldContain(TestData.SkInvoiceItemId31.ToString());
        exception2.Message.ShouldContain(nameof(Entities.InvoiceItem));
        exception2.Message.ShouldContain(TestData.SkInvoiceItemId32.ToString());
        exception3.Message.ShouldContain(nameof(Entities.InvoiceItem));
        exception3.Message.ShouldContain(TestData.SkInvoiceItemId33.ToString());

        items.TotalCount.ShouldBe(1);
        items.Items.Count.ShouldBe(1);
        items.Items[0].Id.ShouldNotBe(TestData.SkInvoiceItemId31);
        items.Items[0].Id.ShouldNotBe(TestData.SkInvoiceItemId32);
        items.Items[0].Id.ShouldNotBe(TestData.SkInvoiceItemId33);
        items.Items[0].Description.ShouldBe("Create");

        report.HasErrors.ShouldBeFalse();
        report.Errors.Count.ShouldBe(0);
    }

    [Fact]
    public async Task Should_Thorw_ManageMany_InvoiceItem_With_Non_Existing_Invoice()
    {
        var guid = _guidGenerator.Create();
        var input = new InvoiceItemManageManyInputDto
        {
            InvoiceId = guid,
            Items =
            [
                Converter.CreateInvoiceItemManageMany(TestData.SkInvoiceItemId31,
                    description: "Update"),
                Converter.CreateInvoiceItemManageMany(description: "Create")
            ],
            ItemsForDelete = [TestData.SkInvoiceItemId31],
            DeleteMissingItems = true
        };

        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _invoiceItemAppService.ManageManyAsync(input);
        });
        exception.Message.ShouldContain(nameof(Entities.Invoice));
        exception.Message.ShouldContain(guid.ToString());
    }
    
    [Fact]
    public async Task Should_ManageMany_InvoiceItem_With_Validation_Errors()
    {
        var input = new InvoiceItemManageManyInputDto
        {
            InvoiceId = TestData.SkInvoiceId3,
            Items =
            [
                Converter.CreateInvoiceItemManageMany(TestData.SkInvoiceItemId31,
                    description: "InvalidUpdate", quantity: -10),
                Converter.CreateInvoiceItemManageMany(TestData.SkInvoiceItemId33,
                    description: "Update33"),
                Converter.CreateInvoiceItemManageMany(description: "Create", quantity: -1)
            ]
        };
        var report = await _invoiceItemAppService.ManageManyAsync(input);
        var items = await _invoiceItemAppService.GetAllAsync(new InvoiceItemGetAllDto
        {
            InvoiceId = TestData.SkInvoiceId3,
            Sorting = "Description"
        });
        
        items.TotalCount.ShouldBe(3);
        items.Items.Count.ShouldBe(3);
        items.Items[0].Id.ShouldBe(TestData.SkInvoiceItemId32);
        items.Items[1].Id.ShouldBe(TestData.SkInvoiceItemId33);
        items.Items[2].Id.ShouldBe(TestData.SkInvoiceItemId31);
        items.Items[0].Description.ShouldBe("Testing a QA");
        items.Items[1].Description.ShouldBe("Update33");
        items.Items[2].Description.ShouldBe("Vývoj webovej aplikácie");

        report.HasErrors.ShouldBeTrue();
        report.Errors.Count.ShouldBe(2);
        report.Errors.ShouldContain("Položku se nepodařilo vytvořit.");
        report.Errors.ShouldContain($"Položku s id={TestData.SkInvoiceItemId31} se nepodařilo zpracovat.");
    }
    
    [Fact]
    public async Task Should_ManageMany_InvoiceItem_With_NotFoundEntity_Errors()
    {
        var guid1 = _guidGenerator.Create();
        var guid2 = _guidGenerator.Create();
        var guid3 = _guidGenerator.Create();
        var input = new InvoiceItemManageManyInputDto
        {
            InvoiceId = TestData.SkInvoiceId3,
            Items =
            [
                Converter.CreateInvoiceItemManageMany(guid1),
                Converter.CreateInvoiceItemManageMany(guid2),
                Converter.CreateInvoiceItemManageMany(description: "Create")
            ],
            ItemsForDelete = [guid3],
            DeleteMissingItems = true
        };
        var report = await _invoiceItemAppService.ManageManyAsync(input);
        var items = await _invoiceItemAppService.GetAllAsync(new InvoiceItemGetAllDto
        {
            InvoiceId = TestData.SkInvoiceId3,
            Sorting = "Description"
        });
        
        items.TotalCount.ShouldBe(1);
        items.Items.Count.ShouldBe(1);
        items.Items[0].Id.ShouldNotBe(TestData.SkInvoiceItemId32);
        items.Items[0].Id.ShouldNotBe(TestData.SkInvoiceItemId33);
        items.Items[0].Id.ShouldNotBe(TestData.SkInvoiceItemId31);
        items.Items[0].Description.ShouldBe("Create");

        report.HasErrors.ShouldBeTrue();
        report.Errors.Count.ShouldBe(3);
        report.Errors.ShouldContain($"Položku s id={guid1} neexistuje na faktuře s id={TestData.SkInvoiceId3}.");
        report.Errors.ShouldContain($"Položku s id={guid2} neexistuje na faktuře s id={TestData.SkInvoiceId3}.");
        report.Errors.ShouldContain($"Položku s id={guid3} neexistuje na faktuře s id={TestData.SkInvoiceId3}.");
    }
}