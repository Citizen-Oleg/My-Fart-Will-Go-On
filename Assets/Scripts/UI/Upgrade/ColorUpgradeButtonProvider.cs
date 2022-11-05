using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UpgradeSystem.UI;

namespace UI.Upgrade
{
    public class ColorUpgradeButtonProvider
    {
        private readonly List<ColorStateButton> _colorStateButtons;
        
        public ColorUpgradeButtonProvider(Settings settings)
        {
            _colorStateButtons = settings.ColorStateButtons;
        }

        public Color GetColorByStateButton(StateUpgradeButton stateButton)
        {
            return _colorStateButtons.FirstOrDefault(state => state.StateButton == stateButton).Color;
        }
        
        [Serializable]
        public class Settings
        {
            public List<ColorStateButton> ColorStateButtons = new List<ColorStateButton>();
        }

        [Serializable]
        public class ColorStateButton
        {
            public StateUpgradeButton StateButton;
            public Color Color;
        }
    }
}