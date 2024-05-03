using ContentSettings.API.Attributes;
using ContentSettings.API.Settings;

namespace Nameplates.Settings
{
    [SettingRegister("Nameplates", "General")]
    public class ColourBasedOnHealth : BoolSetting, ICustomSetting
    {
        public override void ApplyValue()
        {
            NameplateConfig.ColourBasedOnHealth = Value;
        }

        protected override bool GetDefaultValue() => false;

        public string GetDisplayName() => "Colour Nameplates Based On Health";
    }
}