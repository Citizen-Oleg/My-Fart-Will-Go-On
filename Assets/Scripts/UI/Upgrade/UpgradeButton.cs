using System;
using ResourceSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UpgradeSystem.UI
{
    public class UpgradeButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action<UpgradeButton> OnClick;

        public bool IsMax { get; set; }
        
        public TypeUpgrade TypeUpgrade => _typeUpgrade;

        [SerializeField]
        private TypeUpgrade _typeUpgrade;
        [SerializeField]
        private TextMeshProUGUI _textPrice; 
        [SerializeField]
        private TextMeshProUGUI _textLevel;
        [SerializeField]
        private Image _button;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsMax)
            {
                OnClick?.Invoke(this);
            }
        }

        public void SetPriceText(string text)
        {
            _textPrice.text = text;
        }

        public void SetLevel(int level)
        {
            _textLevel.text = "Lvl. " + level;
        }

        public void ChangeColor(Color color)
        {
            _button.color = color;
        }
    }
}