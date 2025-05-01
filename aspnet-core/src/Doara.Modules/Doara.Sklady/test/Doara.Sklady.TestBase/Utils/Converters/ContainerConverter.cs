using Doara.Sklady.Dto.Container;

namespace Doara.Sklady.Utils.Converters;

public static partial class Converter
{
    public const string DefaultContainerName = "Container Name";
    public const string DefaultContainerDescription = "Container Description";
    
    public static ContainerCreateInputDto CreateContainerInput(string name = DefaultContainerName, string description = DefaultContainerDescription)
    {
        return new ContainerCreateInputDto
        {
            Name = name,
            Description = description
        };
    }
    
    public static ContainerUpdateInputDto Convert2UpdateInput(ContainerDetailDto input)
    {
        return new ContainerUpdateInputDto
        {
            Name = input.Name,
            Description = input.Description
        };
    }
}