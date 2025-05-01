using System;
using System.Threading.Tasks;
using Doara.Sklady.Dto.ContainerItem;
using Doara.Sklady.Enums;
using Doara.Sklady.IAppServices;
using Doara.Sklady.Utils.Converters;
using Shouldly;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Xunit;

namespace Doara.Sklady.AppServices;

public class ContainerItemItemAppService_Tests : SkladyApplicationTestBase<SkladyApplicationTestModule>
{
    private readonly IContainerItemAppService _containerItemAppService;
    private readonly IGuidGenerator _guidGenerator;
    
    public ContainerItemItemAppService_Tests()
    {
        _guidGenerator = GetRequiredService<IGuidGenerator>();
        _containerItemAppService = GetRequiredService<IContainerItemAppService>();
    }
    
    [Fact]
    public async Task Should_Get_ContainerItem()
    {
        var containerItem = await _containerItemAppService.GetAsync(TestData.ContainerItem11Id);
        containerItem.Container.Id.ShouldBe(TestData.Container1Id);
        containerItem.Container.Name.ShouldBe("Container1");
        containerItem.Container.Description.ShouldBe("Container1Description");
        containerItem.Id.ShouldBe(TestData.ContainerItem11Id);
        containerItem.State.ShouldBe(ContainerItemState.New);
        containerItem.QuantityType.ShouldBe(QuantityType.Grams);
        containerItem.Name.ShouldBe("ContainerItem11");
        containerItem.Description.ShouldBe("ContainerItem11Description");
        containerItem.PurchaseUrl.ShouldBe("url1");
        containerItem.Quantity.ShouldBe(10);
        containerItem.RealPrice.ShouldBe(10);
        containerItem.PresentationPrice.ShouldBe(10);
        containerItem.Markup.ShouldBe(10);
        containerItem.MarkupRate.ShouldBe(10);
        containerItem.Discount.ShouldBe(10);
        containerItem.DiscountRate.ShouldBe(10);
        containerItem.Id.ShouldBe(TestData.ContainerItem11Id);
    }
    
    [Fact]
    public async Task Should_Throw_Get_Non_Existing_ContainerItem()
    {
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _containerItemAppService.GetAsync(id);
        });
        exception.Message.ShouldContain(nameof(Entities.ContainerItem));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Create_ContainerItem()
    {
        var input = Converter.CreateContainerItemInput(TestData.Container2Id);
        var containerItem = await _containerItemAppService.CreateAsync(input);
        
        containerItem.Container.Id.ShouldBe(TestData.Container2Id);
        containerItem.Container.Name.ShouldBe("Container2");
        containerItem.Container.Description.ShouldBe("Container2Description");
        containerItem.Id.ShouldNotBe(Guid.Empty);
        containerItem.State.ShouldBe(ContainerItemState.New);
        containerItem.QuantityType.ShouldBe(Converter.DefaultContainerItemQuantityType);
        containerItem.Name.ShouldBe(Converter.DefaultContainerItemName);
        containerItem.Description.ShouldBe(Converter.DefaultContainerItemDescription);
        containerItem.PurchaseUrl.ShouldBe(Converter.DefaultContainerItemPurchaseUrl);
        containerItem.Quantity.ShouldBe(Converter.DefaultContainerItemQuantity);
        containerItem.RealPrice.ShouldBe(Converter.DefaultContainerItemRealPrice);
        containerItem.PresentationPrice.ShouldBe(Converter.DefaultContainerItemRealPrice);
        containerItem.Markup.ShouldBe(Converter.DefaultContainerItemMarkup);
        containerItem.MarkupRate.ShouldBe(Converter.DefaultContainerItemMarkupRate);
        containerItem.Discount.ShouldBe(Converter.DefaultContainerItemDiscount);
        containerItem.DiscountRate.ShouldBe(Converter.DefaultContainerItemDiscountRate);
    }
    
    [Fact]
    public async Task Should_Throw_Create_Non_Existing_Container()
    {
        var id = _guidGenerator.Create();
        var input = Converter.CreateContainerItemInput(id);
        
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _containerItemAppService.CreateAsync(input);
        });
        exception.Message.ShouldContain(nameof(Entities.Container));
        exception.Message.ShouldContain(id.ToString());
    }

    [Fact]
    public async Task Should_Update_ContainerItem()
    {
        var containerItem = await _containerItemAppService.GetAsync(TestData.ContainerItem11Id);
        containerItem.Name = Converter.DefaultContainerItemName;
        containerItem.Description = Converter.DefaultContainerItemDescription;
        containerItem.State = ContainerItemState.Used;
        containerItem.QuantityType = Converter.DefaultContainerItemQuantityType;
        containerItem.Name = Converter.DefaultContainerItemName;
        containerItem.Description = Converter.DefaultContainerItemDescription;
        containerItem.PurchaseUrl = Converter.DefaultContainerItemPurchaseUrl;
        containerItem.Quantity = Converter.DefaultContainerItemQuantity;
        containerItem.RealPrice = Converter.DefaultContainerItemRealPrice;
        containerItem.Markup = Converter.DefaultContainerItemMarkup;
        containerItem.MarkupRate = Converter.DefaultContainerItemMarkupRate;
        containerItem.Discount = Converter.DefaultContainerItemDiscount;
        containerItem.DiscountRate = Converter.DefaultContainerItemDiscountRate;
        containerItem.Container.Id = TestData.Container3Id;
        
        var updatedContainerItem = await _containerItemAppService.UpdateAsync(containerItem.Id, Converter.Convert2UpdateInput(containerItem));
        
        updatedContainerItem.Id.ShouldBe(containerItem.Id);
        updatedContainerItem.Name.ShouldBe(Converter.DefaultContainerItemName);
        updatedContainerItem.Description.ShouldBe(Converter.DefaultContainerItemDescription);
        updatedContainerItem.State.ShouldBe(ContainerItemState.Used);
        updatedContainerItem.QuantityType.ShouldBe(Converter.DefaultContainerItemQuantityType);
        updatedContainerItem.PurchaseUrl.ShouldBe(Converter.DefaultContainerItemPurchaseUrl);
        updatedContainerItem.Quantity.ShouldBe(Converter.DefaultContainerItemQuantity);
        updatedContainerItem.RealPrice.ShouldBe(Converter.DefaultContainerItemRealPrice);
        updatedContainerItem.Markup.ShouldBe(Converter.DefaultContainerItemMarkup);
        updatedContainerItem.MarkupRate.ShouldBe(Converter.DefaultContainerItemMarkupRate);
        updatedContainerItem.Discount.ShouldBe(Converter.DefaultContainerItemDiscount);
        updatedContainerItem.DiscountRate.ShouldBe(Converter.DefaultContainerItemDiscountRate);
        
        updatedContainerItem.Container.Id.ShouldBe(TestData.Container3Id);
        updatedContainerItem.Container.Name.ShouldBe("Container3");
        updatedContainerItem.Container.Description.ShouldBe("Container3Description");
    }
    
    [Fact]
    public async Task Should_Throw_Update_Non_Existing_ContainerItem()
    {
        var id = _guidGenerator.Create();
        var containerItem = await _containerItemAppService.GetAsync(TestData.ContainerItem11Id);
        var input = Converter.Convert2UpdateInput(containerItem);
        
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _containerItemAppService.UpdateAsync(id, input);
        });
        exception.Message.ShouldContain(nameof(Entities.ContainerItem));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Delete_ContainerItem()
    {
        await _containerItemAppService.DeleteAsync(TestData.ContainerItem11Id);

        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _containerItemAppService.GetAsync(TestData.ContainerItem11Id);
        });
        exception.Message.ShouldContain(nameof(Entities.ContainerItem));
        exception.Message.ShouldContain(TestData.ContainerItem11Id.ToString());
    }
    
    [Fact]
    public async Task Should_Throw_Delete_Non_Existing_ContainerItem()
    {
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _containerItemAppService.DeleteAsync(id);
        });
        exception.Message.ShouldContain(nameof(Entities.ContainerItem));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_GetAll()
    {
        var entities = await _containerItemAppService.GetAllAsync(new ContainerItemGetAllDto
        {
            SkipCount = 1,
            MaxResultCount = 4,
            Sorting = "Name ascending",
            ContainerId = TestData.Container2Id
        });
        
        entities.TotalCount.ShouldBe(2);
        entities.Items.Count.ShouldBe(1);
        entities.Items[0].Id.ShouldBe(TestData.ContainerItem22Id);
    }
    
    [Fact]
    public async Task Should_GetAllWithDetail()
    {
        var entities = await _containerItemAppService.GetAllWithDetailAsync(new ContainerItemGetAllDto
        {
            SkipCount = 1,
            MaxResultCount = 4,
            Sorting = "Name ascending",
            ContainerId = TestData.Container2Id
        });
        
        entities.TotalCount.ShouldBe(2);
        entities.Items.Count.ShouldBe(1);
        entities.Items[0].Id.ShouldBe(TestData.ContainerItem22Id);
        entities.Items[0].Container.Id.ShouldBe(TestData.Container2Id);
        entities.Items[0].Container.Name.ShouldBe("Container2");
        entities.Items[0].Container.Description.ShouldBe("Container2Description");
    }
}