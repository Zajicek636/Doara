using System;
using System.Linq;
using System.Threading.Tasks;
using Doara.Sklady.Enums;
using Doara.Sklady.IAppServices;
using Doara.Sklady.Utils.Converters;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Xunit;

namespace Doara.Sklady.AppServices;

public class ContainerAppService_Tests : SkladyApplicationTestBase<SkladyApplicationTestModule>
{
    private readonly IContainerAppService _containerAppService;
    private readonly IGuidGenerator _guidGenerator;
    
    public ContainerAppService_Tests()
    {
        _guidGenerator = GetRequiredService<IGuidGenerator>();
        _containerAppService = GetRequiredService<IContainerAppService>();
    }
    
    [Fact]
    public async Task Should_Get_Container()
    {
        var container = await _containerAppService.GetAsync(TestData.Container1Id);
        container.Id.ShouldBe(TestData.Container1Id);
        container.Name.ShouldBe("Container1");
        container.Description.ShouldBe("Container1Description");
        container.Items.Count.ShouldBe(1);
        var item = container.Items[0];
        item.Id.ShouldBe(TestData.ContainerItem11Id);
        item.State.ShouldBe(ContainerItemState.New);
        item.QuantityType.ShouldBe(QuantityType.Grams);
        item.Name.ShouldBe("ContainerItem11");
        item.Description.ShouldBe("ContainerItem11Description");
        item.PurchaseUrl.ShouldBe("url1");
        item.Quantity.ShouldBe(10);
        item.RealPrice.ShouldBe(10);
        item.PresentationPrice.ShouldBe(10);
        item.Markup.ShouldBe(10);
        item.MarkupRate.ShouldBe(10);
        item.Discount.ShouldBe(10);
        item.DiscountRate.ShouldBe(10);
        item.ContainerId.ShouldBe(TestData.Container1Id);
    }
    
    [Fact]
    public async Task Should_Throw_Get_Non_Existing_Container()
    {
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _containerAppService.GetAsync(id);
        });
        exception.Message.ShouldContain(nameof(Entities.Container));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Create_Container()
    {
        var input = Converter.CreateContainerInput();
        var container = await _containerAppService.CreateAsync(input);
        
        container.Id.ShouldNotBe(Guid.Empty);
        container.Name.ShouldBe(input.Name);
        container.Description.ShouldBe(input.Description);
        container.Items.Count.ShouldBe(0);
    }

    [Fact]
    public async Task Should_Update_Container()
    {
        var container = await _containerAppService.GetAsync(TestData.Container1Id);
        container.Name = Converter.DefaultContainerName;
        container.Description = Converter.DefaultContainerDescription;
        
        var updatedContainer = await _containerAppService.UpdateAsync(container.Id, Converter.Convert2UpdateInput(container));
        
        updatedContainer.Id.ShouldBe(container.Id);
        updatedContainer.Name.ShouldBe(Converter.DefaultContainerName);
        updatedContainer.Description.ShouldBe(Converter.DefaultContainerDescription);
        updatedContainer.Items.Count.ShouldBe(1);
        updatedContainer.Items.First().Id.ShouldBe(TestData.ContainerItem11Id);
    }
    
    [Fact]
    public async Task Should_Throw_Update_Non_Existing_Container()
    {
        var id = _guidGenerator.Create();
        var container = await _containerAppService.GetAsync(TestData.Container1Id);
        container.Name = Converter.DefaultContainerName;
        container.Description = Converter.DefaultContainerDescription;
        
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _containerAppService.UpdateAsync(id, Converter.Convert2UpdateInput(container));
        });
        exception.Message.ShouldContain(nameof(Entities.Container));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Delete_Container()
    {
        await _containerAppService.DeleteAsync(TestData.Container1Id);

        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _containerAppService.GetAsync(TestData.Container1Id);
        });
        exception.Message.ShouldContain(nameof(Entities.Container));
        exception.Message.ShouldContain(TestData.Container1Id.ToString());
    }
    
    [Fact]
    public async Task Should_Throw_Delete_Non_Existing_Container()
    {
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _containerAppService.DeleteAsync(id);
        });
        exception.Message.ShouldContain(nameof(Entities.Container));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_GetAll()
    {
        var entities = await _containerAppService.GetAllAsync(new PagedAndSortedResultRequestDto
        {
            SkipCount = 1,
            MaxResultCount = 4,
            Sorting = "Name ascending"
        });
        
        entities.TotalCount.ShouldBe(3);
        entities.Items.Count.ShouldBe(2);
        entities.Items[0].Id.ShouldBe(TestData.Container2Id);
        entities.Items[1].Id.ShouldBe(TestData.Container3Id);
    }
    
    [Fact]
    public async Task Should_GetAllWithDetail()
    {
        var entities = await _containerAppService.GetAllWithDetailAsync(new PagedAndSortedResultRequestDto
        {
            SkipCount = 1,
            MaxResultCount = 4,
            Sorting = "Name ascending"
        });
        
        entities.TotalCount.ShouldBe(3);
        entities.Items.Count.ShouldBe(2);
        entities.Items[0].Id.ShouldBe(TestData.Container2Id);
        entities.Items[0].Items.Count.ShouldBe(2);
        entities.Items[0].Items[0].Id.ShouldBe(TestData.ContainerItem21Id);
        entities.Items[0].Items[1].Id.ShouldBe(TestData.ContainerItem22Id);
        entities.Items[1].Id.ShouldBe(TestData.Container3Id);
        entities.Items[1].Items.Count.ShouldBe(0);
    }
}