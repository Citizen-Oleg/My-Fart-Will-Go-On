using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.ItemShop
{
    public class ColorItemButtonProvider
    {
        private readonly List<ColorItemButton> _colorItemButtons;
        
        public ColorItemButtonProvider(Settings settings)
        {
            _colorItemButtons = settings.ColorItemButtons;
        }

        public Color GetColorByButtonState(ItemButtonState itemButtonState)
        {
            return _colorItemButtons.FirstOrDefault(button => button.ItemButtonState == itemButtonState).Color;
        }
        
        [Serializable]
        public class Settings
        {
            public List<ColorItemButton> ColorItemButtons = new List<ColorItemButton>();
        }

        [Serializable]
        public class ColorItemButton
        {
            public ItemButtonState ItemButtonState;
            public Color Color;
        }
    }

    public enum ItemButtonState
    {
        Active = 0,
        Block = 1
    }
}