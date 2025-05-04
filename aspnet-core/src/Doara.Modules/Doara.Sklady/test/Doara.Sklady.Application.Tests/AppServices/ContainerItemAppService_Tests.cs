using System;
using System.Linq;
using System.Threading.Tasks;
using Doara.Sklady.Dto.ContainerItem;
using Doara.Sklady.Dto.StockMovement;
using Doara.Sklady.Enums;
using Doara.Sklady.IAppServices;
using Doara.Sklady.Utils.Converters;
using Shouldly;
using Volo.Abp;
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
        containerItem.QuantityType.ShouldBe(QuantityType.Grams);
        containerItem.Name.ShouldBe("ContainerItem11");
        containerItem.Description.ShouldBe("ContainerItem11Description");
        containerItem.PurchaseUrl.ShouldBe("url1");
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
        containerItem.QuantityType.ShouldBe(Converter.DefaultContainerItemQuantityType);
        containerItem.Name.ShouldBe(Converter.DefaultContainerItemName);
        containerItem.Description.ShouldBe(Converter.DefaultContainerItemDescription);
        containerItem.PurchaseUrl.ShouldBe(Converter.DefaultContainerItemPurchaseUrl);
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
        containerItem.QuantityType = Converter.DefaultContainerItemQuantityType;
        containerItem.Name = Converter.DefaultContainerItemName;
        containerItem.Description = Converter.DefaultContainerItemDescription;
        containerItem.PurchaseUrl = Converter.DefaultContainerItemPurchaseUrl;
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
        updatedContainerItem.QuantityType.ShouldBe(Converter.DefaultContainerItemQuantityType);
        updatedContainerItem.PurchaseUrl.ShouldBe(Converter.DefaultContainerItemPurchaseUrl);
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

    [Fact]
    public async Task Should_AddStock()
    {
        var result = await _containerItemAppService.AddStockAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 5
        });
        
        result.Available.ShouldBe(5);
        result.OnHand.ShouldBe(5);
        result.Reserved.ShouldBe(0);
        result.Movements.Count.ShouldBe(1);
        result.Movements[0].Id.ShouldNotBe(Guid.Empty);
        result.Movements[0].Quantity.ShouldBe(5);
        result.Movements[0].MovementCategory.ShouldBe(MovementCategory.Unused);
        result.Movements[0].RelatedDocumentId.ShouldBeNull();
    }
    
    [Fact]
    public async Task Should_Throw_AddStock_Non_Existing_ContainerItem()
    {
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _containerItemAppService.AddStockAsync(id, new StockMovementCreateInputDto
            {
                Quantity = 5
            });
        });
        exception.Message.ShouldContain(nameof(Entities.ContainerItem));
        exception.Message.ShouldContain(id.ToString());
    }

    [Fact]
    public async Task Should_ReserveItem()
    {
        await _containerItemAppService.AddStockAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 5
        });
        
        var result = await _containerItemAppService.ReserveItemAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 5
        });
        
        result.Available.ShouldBe(0);
        result.OnHand.ShouldBe(5);
        result.Reserved.ShouldBe(5);
        result.Movements.Count.ShouldBe(2);
        result.Movements[0].Id.ShouldNotBe(Guid.Empty);
        result.Movements[0].Quantity.ShouldBe(5);
        result.Movements[0].MovementCategory.ShouldBe(MovementCategory.Unused);
        result.Movements[0].RelatedDocumentId.ShouldBeNull();
        result.Movements[1].Id.ShouldNotBe(Guid.Empty);
        result.Movements[1].Quantity.ShouldBe(5);
        result.Movements[1].MovementCategory.ShouldBe(MovementCategory.Reserved);
        result.Movements[1].RelatedDocumentId.ShouldBeNull();
    }

    [Fact]
    public async Task Should_Throw_ReserveItem_Non_Existing_ContainerItem()
    {
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _containerItemAppService.ReserveItemAsync(id, new StockMovementCreateInputDto
            {
                Quantity = 5
            });
        });
        exception.Message.ShouldContain(nameof(Entities.ContainerItem));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Throw_ReserveItem_Without_Enough_Resources()
    {
        await _containerItemAppService.AddStockAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 5
        });

        var documentGuid = _guidGenerator.Create();
        await _containerItemAppService.ReserveItemAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 4,
            RelatedDocumentId = documentGuid
        });
        
        var exception = await Should.ThrowAsync<BusinessException>(async () =>
        {
            await _containerItemAppService.ReserveItemAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
            {
                Quantity = 2
            });
        });
        var item = await _containerItemAppService.GetAsync(TestData.ContainerItem22Id);
        
        exception.Code.ShouldBe(SkladyErrorCodes.LackOfAvailableResources);
        item.Available.ShouldBe(1);
        item.OnHand.ShouldBe(5);
        item.Reserved.ShouldBe(4);
        item.Movements.Count.ShouldBe(2);
        item.Movements[0].Id.ShouldNotBe(Guid.Empty);
        item.Movements[0].Quantity.ShouldBe(5);
        item.Movements[0].MovementCategory.ShouldBe(MovementCategory.Unused);
        item.Movements[0].RelatedDocumentId.ShouldBeNull();
        item.Movements[1].Id.ShouldNotBe(Guid.Empty);
        item.Movements[1].Quantity.ShouldBe(4);
        item.Movements[1].MovementCategory.ShouldBe(MovementCategory.Reserved);
        item.Movements[1].RelatedDocumentId.ShouldBe(documentGuid);
    }
    
    [Fact]
    public async Task Should_UseItem()
    {
        await _containerItemAppService.AddStockAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 5
        });
        
        var result = await _containerItemAppService.UseItemAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 5
        });
        
        result.Available.ShouldBe(0);
        result.OnHand.ShouldBe(0);
        result.Reserved.ShouldBe(0);
        result.Movements.Count.ShouldBe(2);
        result.Movements[0].Id.ShouldNotBe(Guid.Empty);
        result.Movements[0].Quantity.ShouldBe(5);
        result.Movements[0].MovementCategory.ShouldBe(MovementCategory.Unused);
        result.Movements[0].RelatedDocumentId.ShouldBeNull();
        result.Movements[1].Id.ShouldNotBe(Guid.Empty);
        result.Movements[1].Quantity.ShouldBe(5);
        result.Movements[1].MovementCategory.ShouldBe(MovementCategory.Used);
        result.Movements[1].RelatedDocumentId.ShouldBeNull();
    }

    [Fact]
    public async Task Should_Throw_UseItem_Non_Existing_ContainerItem()
    {
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _containerItemAppService.UseItemAsync(id, new StockMovementCreateInputDto
            {
                Quantity = 5
            });
        });
        exception.Message.ShouldContain(nameof(Entities.ContainerItem));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Throw_UseItem_Without_Enough_Resources()
    {
        await _containerItemAppService.AddStockAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 5
        });
        
        await _containerItemAppService.UseItemAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 4
        });
        
        var exception = await Should.ThrowAsync<BusinessException>(async () =>
        {
            await _containerItemAppService.UseItemAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
            {
                Quantity = 2
            });
        });
        var item = await _containerItemAppService.GetAsync(TestData.ContainerItem22Id);
        
        exception.Code.ShouldBe(SkladyErrorCodes.LackOfAvailableResources);
        item.Available.ShouldBe(1);
        item.OnHand.ShouldBe(1);
        item.Reserved.ShouldBe(0);
        item.Movements.Count.ShouldBe(2);
        item.Movements[0].Id.ShouldNotBe(Guid.Empty);
        item.Movements[0].Quantity.ShouldBe(5);
        item.Movements[0].MovementCategory.ShouldBe(MovementCategory.Unused);
        item.Movements[0].RelatedDocumentId.ShouldBeNull();
        item.Movements[1].Id.ShouldNotBe(Guid.Empty);
        item.Movements[1].Quantity.ShouldBe(4);
        item.Movements[1].MovementCategory.ShouldBe(MovementCategory.Used);
        item.Movements[1].RelatedDocumentId.ShouldBeNull();
    }
    
    [Fact]
    public async Task Should_ConvertReservationToUsage()
    {
        await _containerItemAppService.AddStockAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 5
        });
        
        var documentGuid = _guidGenerator.Create();
        var containerItem = await _containerItemAppService.ReserveItemAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 5,
            RelatedDocumentId = documentGuid
        });

        var result = await _containerItemAppService.ConvertReservationToUsageAsync(TestData.ContainerItem22Id, containerItem.Movements[1].Id);
        
        result.Available.ShouldBe(0);
        result.OnHand.ShouldBe(0);
        result.Reserved.ShouldBe(0);
        result.Movements.Count.ShouldBe(3);
        result.Movements[0].Id.ShouldNotBe(Guid.Empty);
        result.Movements[0].Quantity.ShouldBe(5);
        result.Movements[0].MovementCategory.ShouldBe(MovementCategory.Unused);
        result.Movements[0].RelatedDocumentId.ShouldBeNull();
        result.Movements[1].Id.ShouldNotBe(Guid.Empty);
        result.Movements[1].Quantity.ShouldBe(5);
        result.Movements[1].MovementCategory.ShouldBe(MovementCategory.Reserved2Used);
        result.Movements[1].RelatedDocumentId.ShouldBe(documentGuid);
        result.Movements[2].Id.ShouldNotBe(Guid.Empty);
        result.Movements[2].Quantity.ShouldBe(5);
        result.Movements[2].MovementCategory.ShouldBe(MovementCategory.Used);
        result.Movements[2].RelatedDocumentId.ShouldBe(documentGuid);
    }

    [Fact]
    public async Task Should_Throw_ConvertReservationToUsage_Non_Existing_ContainerItem()
    {
        var id = _guidGenerator.Create();
        var movementId = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _containerItemAppService.ConvertReservationToUsageAsync(id, movementId);
        });
        exception.Message.ShouldContain(nameof(Entities.ContainerItem));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Throw_ConvertReservationToUsage_Non_Existing_StockMovement()
    {
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _containerItemAppService.ConvertReservationToUsageAsync(TestData.ContainerItem22Id, id);
        });
        exception.Message.ShouldContain(nameof(Entities.StockMovement));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Throw_ConvertReservationToUsage_Without_Reservation_StockMovement()
    {
        await _containerItemAppService.AddStockAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 5
        });
        
        var containerItem = await _containerItemAppService.UseItemAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 5
        });
        
        var exception = await Should.ThrowAsync<BusinessException>(async () =>
        {
            await _containerItemAppService.ConvertReservationToUsageAsync(TestData.ContainerItem22Id, containerItem.Movements[1].Id);
        });
        exception.Code.ShouldBe(SkladyErrorCodes.MovementIsNotReservation);
    }

    [Fact]
    public async Task Should_RemoveMovement()
    {
        var item = await _containerItemAppService.AddStockAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 5
        });

        var result = await _containerItemAppService.RemoveMovementAsync(TestData.ContainerItem22Id, item.Movements[0].Id);
        result.Available.ShouldBe(0);
        result.OnHand.ShouldBe(0);
        result.Reserved.ShouldBe(0);
        result.Movements.Count.ShouldBe(0);
    }
    
    [Fact]
    public async Task Should_Throw_RemoveMovement_Without_Enough_Resources()
    {
        await _containerItemAppService.AddStockAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 1
        });
        
        await _containerItemAppService.AddStockAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 2
        });
        
        var item = await _containerItemAppService.ReserveItemAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 2
        });
        
        var exception = await Should.ThrowAsync<BusinessException>(async () =>
        {
            await _containerItemAppService.RemoveMovementAsync(TestData.ContainerItem22Id, item.Movements.First(x => x is { Quantity: 2, MovementCategory: MovementCategory.Unused }).Id);
        });
        exception.Code.ShouldBe(SkladyErrorCodes.LackOfAvailableResources);
    }
    
    [Fact]
    public async Task Should_Throw_RemoveMovement_Non_Existing_ContainerItem()
    {
        var id = _guidGenerator.Create();
        var movementId = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _containerItemAppService.RemoveMovementAsync(id, movementId);
        });
        exception.Message.ShouldContain(nameof(Entities.ContainerItem));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Throw_RemoveMovement_Non_Existing_StockMovement()
    {
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _containerItemAppService.RemoveMovementAsync(TestData.ContainerItem22Id, id);
        });
        exception.Message.ShouldContain(nameof(Entities.StockMovement));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Throw_RemoveMovement_Reserved2Used()
    {
        await _containerItemAppService.AddStockAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 5
        });
        
        var documentGuid = _guidGenerator.Create();
        var containerItem = await _containerItemAppService.ReserveItemAsync(TestData.ContainerItem22Id, new StockMovementCreateInputDto
        {
            Quantity = 5,
            RelatedDocumentId = documentGuid
        });

        var item = await _containerItemAppService.ConvertReservationToUsageAsync(TestData.ContainerItem22Id, containerItem.Movements[1].Id);
        
        var exception = await Should.ThrowAsync<BusinessException>(async () =>
        {
            await _containerItemAppService.RemoveMovementAsync(TestData.ContainerItem22Id, item.Movements.First(x => x.MovementCategory == MovementCategory.Reserved2Used).Id);
        });
        exception.Code.ShouldBe(SkladyErrorCodes.MovementCannotBeRemoved);
    }
}