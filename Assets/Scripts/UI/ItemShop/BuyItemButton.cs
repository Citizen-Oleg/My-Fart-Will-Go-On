using System;
using ItemSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.ItemShop
{
    public class BuyItemButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action<BuyItemButton> OnClick;

        public TypeItem TypeItem => _typeItem;
        
        [SerializeField]
        private TypeItem _typeItem;
        [SerializeField]
        private TextMeshProUGUI _priceText;
        [SerializeField]
        private Image _button;
        
        private bool _isActive;

        public void SetPrice(string priceText)
        {
            _priceText.text = priceText;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isActive)
            {
                OnClick?.Invoke(this);
            }
        }
        
        public void SetActive(bool isActive)
        {
            _isActive = isActive;
        }

        public void SetColor(Color color)
        {
            _button.color = color;
        }
    }
}