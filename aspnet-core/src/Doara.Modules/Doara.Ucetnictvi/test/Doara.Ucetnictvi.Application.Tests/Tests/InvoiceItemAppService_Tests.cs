using System.Threading.Tasks;
using Doara.Sklady;
using Doara.Sklady.Enums;
using Doara.Sklady.IAppServices;
using Doara.Ucetnictvi.Dto.InvoiceItem;
using Doara.Ucetnictvi.Enums;
using Doara.Ucetnictvi.IAppServices;
using Doara.Ucetnictvi.Utils.Converters;
using Shouldly;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Xunit;

namespace Doara.Ucetnictvi.Tests;

public class InvoiceItemAppService_Tests : UcetnictviApplicationTestBase<UcetnictviApplicationTestModule>
{
    private readonly IInvoiceItemAppService _invoiceItemAppService;
    private readonly IContainerItemAppService _containerItemAppService;
    private readonly IGuidGenerator _guidGenerator;
    
    public InvoiceItemAppService_Tests()
    {
        _invoiceItemAppService = GetRequiredService<IInvoiceItemAppService>();
        _guidGenerator = GetRequiredService<IGuidGenerator>();
        _containerItemAppService = GetRequiredService<IContainerItemAppService>();
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
            ItemsForDelete = [TestData.SkInvoiceItemId32]
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
            ItemsForDelete = [TestData.SkInvoiceItemId31]
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
            ItemsForDelete = [guid3, TestData.SkInvoiceItemId31, TestData.SkInvoiceItemId32, TestData.SkInvoiceItemId33]
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
        report.Errors.ShouldContain($"Položka s id={guid1} neexistuje na faktuře s id={TestData.SkInvoiceId3}.");
        report.Errors.ShouldContain($"Položka s id={guid2} neexistuje na faktuře s id={TestData.SkInvoiceId3}.");
        report.Errors.ShouldContain($"Položka s id={guid3} neexistuje na faktuře s id={TestData.SkInvoiceId3}.");
    }

    [Fact]
    public async Task Should_ManageMany_InvoiceItem_With_StockMovements()
    {
        var input = new InvoiceItemManageManyInputDto
        {
            InvoiceId = TestData.SkInvoiceId3,
            Items =
            [
                Converter.CreateInvoiceItemManageMany(null, TestData.ContainerItem11Id,
                    description: "CreateWithMovement"),
                Converter.CreateInvoiceItemManageMany(TestData.SkInvoiceItemId33, TestData.ContainerItem12Id, description: "Update", quantity: 50),
                Converter.CreateInvoiceItemManageMany(description: "Create")
            ],
            ItemsForDelete = [TestData.SkInvoiceItemId31, TestData.SkInvoiceItemId32]
        };
        var report = await _invoiceItemAppService.ManageManyAsync(input);
        var containerItem11 = await _containerItemAppService.GetAsync(TestData.ContainerItem11Id);
        var containerItem12 =  await _containerItemAppService.GetAsync(TestData.ContainerItem12Id);
        
        report.HasErrors.ShouldBeFalse();
        report.Errors.Count.ShouldBe(0);
        
        containerItem11.Movements.Count.ShouldBe(2);
        containerItem11.Movements[0].RelatedDocumentId.ShouldBeNull();
        containerItem11.Movements[0].Quantity.ShouldBe(100);
        containerItem11.Movements[0].MovementCategory.ShouldBe(MovementCategory.Unused);
        containerItem11.Movements[1].RelatedDocumentId.ShouldBe(TestData.SkInvoiceId3);
        containerItem11.Movements[1].Quantity.ShouldBe(Converter.DefaultInvoiceItemQuantity);
        containerItem11.Movements[1].MovementCategory.ShouldBe(MovementCategory.Reserved);
        
        containerItem12.Movements.Count.ShouldBe(2);
        containerItem12.Movements[0].RelatedDocumentId.ShouldBeNull();
        containerItem12.Movements[0].Quantity.ShouldBe(100);
        containerItem12.Movements[0].MovementCategory.ShouldBe(MovementCategory.Unused);
        containerItem12.Movements[1].RelatedDocumentId.ShouldBe(TestData.SkInvoiceId3);
        containerItem12.Movements[1].Quantity.ShouldBe(50);
        containerItem12.Movements[1].MovementCategory.ShouldBe(MovementCategory.Reserved);
    }
    
    [Fact]
    public async Task Should_Throw_ManageMany_InvoiceItem_Without_Resources()
    {
        var input = new InvoiceItemManageManyInputDto
        {
            InvoiceId = TestData.SkInvoiceId3,
            Items =
            [
                Converter.CreateInvoiceItemManageMany(null, TestData.ContainerItem11Id,
                    description: "CreateWithMovement"),
                Converter.CreateInvoiceItemManageMany(TestData.SkInvoiceItemId33, TestData.ContainerItem12Id, description: "Update", quantity: 101),
                Converter.CreateInvoiceItemManageMany(description: "Create")
            ],
            ItemsForDelete = [TestData.SkInvoiceItemId31, TestData.SkInvoiceItemId32]
        };
        
        var exception = await Should.ThrowAsync<BusinessException>(async () =>
        {
            await _invoiceItemAppService.ManageManyAsync(input);
        });
        exception.Code.ShouldBe(SkladyErrorCodes.LackOfAvailableResources);
        
        var containerItem11 = await _containerItemAppService.GetAsync(TestData.ContainerItem11Id);
        var containerItem12 =  await _containerItemAppService.GetAsync(TestData.ContainerItem12Id);
        
        containerItem11.Movements.Count.ShouldBe(1);
        containerItem11.Movements[0].RelatedDocumentId.ShouldBeNull();
        containerItem11.Movements[0].Quantity.ShouldBe(100);
        containerItem11.Movements[0].MovementCategory.ShouldBe(MovementCategory.Unused);
        
        containerItem12.Movements.Count.ShouldBe(1);
        containerItem12.Movements[0].RelatedDocumentId.ShouldBeNull();
        containerItem12.Movements[0].Quantity.ShouldBe(100);
        containerItem12.Movements[0].MovementCategory.ShouldBe(MovementCategory.Unused);
    }
}