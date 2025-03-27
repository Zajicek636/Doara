using Volo.Abp.Settings;

namespace Doara.Settings;

public class DoaraSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(DoaraSettings.MySetting1));
    }
}
