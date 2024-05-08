using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Zorro.Core.CLI;

namespace Nameplates
{
    internal class NP
    {
        [ConsoleCommand]
        public static void ToggleDisplayHealth() => NameplateConfig.DisplayHealth = !NameplateConfig.DisplayHealth;

        [ConsoleCommand]
        public static void ToggleHealthNewLine() => NameplateConfig.HealthNewLine = !NameplateConfig.HealthNewLine;

        [ConsoleCommand]
        public static void ToggleNameplates() => NameplateConfig.ShowNameplates = !NameplateConfig.ShowNameplates;

        [ConsoleCommand]
        public static void ToggleUseColors() => NameplateConfig.UseHealthColors = !NameplateConfig.UseHealthColors;

        [ConsoleCommand]
        public static void SetFontSize(float size) => NameplateConfig.FontSize = size;

        [ConsoleCommand]
        public static void SetHeightOffset(float offset) => NameplateConfig.HeightOffset = offset;
    }
}
