using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nameplates
{
    internal class NameplateConfig
    {
        private static ConfigEntry<bool> _showNameplates;
        private static ConfigEntry<bool> _displayHealth;
        private static ConfigEntry<bool> _healthNewLine;
        private static ConfigEntry<bool> _useHealthColors;
        private static ConfigEntry<float> _fontSize;
        private static ConfigEntry<float> _heightOffset;

        public static bool ShowNameplates
        {
            get => _showNameplates.Value;
            set => _showNameplates.Value = value;
        }
        public static bool DisplayHealth
        {
            get => _displayHealth.Value;
            set => _displayHealth.Value = value;
        }
        public static bool HealthNewLine
        {
            get => _healthNewLine.Value;
            set => _healthNewLine.Value = value;
        }
        public static bool UseHealthColors
        {
            get => _useHealthColors.Value;
            set => _useHealthColors.Value = value;
        }

        public static float FontSize
        {
            get => _fontSize.Value;
            set => _fontSize.Value = value;
        }
        public static float HeightOffset
        {
            get => _heightOffset.Value;
            set => _heightOffset.Value = value;
        }

        internal static void Bind(ConfigFile config)
        {
            _showNameplates = config.Bind("General", "Show Nameplates", true, "Show nameplates above players");
            _displayHealth = config.Bind("General", "Display Health", true, "Display player health in nameplates");
            _healthNewLine = config.Bind("General", "Health on New Line", false, "Display player health on a new line");
            _useHealthColors = config.Bind("General", "Use Health Colors", false, "Use colors to indicate player health");
            _fontSize = config.Bind("General", "Font Size", 1.5f, "Font size of the nameplate text");
            _heightOffset = config.Bind("General", "Height Offset", 0.5f, "How high above the players head the nameplate will be");
        }

    }
}
