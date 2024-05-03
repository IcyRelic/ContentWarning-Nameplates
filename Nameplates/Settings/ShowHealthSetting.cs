using ContentSettings.API.Attributes;
using ContentSettings.API.Settings;

namespace Nameplates.Settings
{
    [SettingRegister("Nameplates", "General")]
    public class ShowHealthSetting : BoolSetting, ICustomSetting
    {
        public override void ApplyValue()
        {
            NameplateConfig.ShowHealth = Value;
        }

        protected override bool GetDefaultValue() => false;

        public string GetDisplayName() => "Show Health on Nameplates";
    }
}