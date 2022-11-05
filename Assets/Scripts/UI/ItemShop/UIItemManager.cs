using System;
using System.Collections.Generic;
using System.Linq;
using ItemSystem;
using PlayerComponent;
using ResourceSystem;

namespace UI.ItemShop
{
    public class UIItemManager : IDisposable
    {
        private readonly PlayerItemInstaller _playerItemInstaller;
        private readonly ItemManager _itemManager;
        private readonly ColorItemButtonProvider _colorItemButtonProvider;
        private readonly ResourceManagerLevel _resourceManagerLevel;

        private readonly List<BuyItemButton> _buyItemButtons;

        public UIItemManager(PlayerItemInstaller playerItemInstaller, ItemManager itemManager, ColorItemButtonProvider colorItemButtonProvider,
            ResourceManagerLevel resourceManagerLevel, Settings settings)
        {
            _playerItemInstaller = playerItemInstaller;
            _itemManager = itemManager;
            _colorItemButtonProvider = colorItemButtonProvider;
            _resourceManagerLevel = resourceManagerLevel;

            _buyItemButtons = settings.BuyItemButtons;

            SetPriceBuyButtons();
            SetColorBuyButtons();
            ActivateBuyButton();
            
            _resourceManagerLevel.OnResourceChange += RefreshColorButton;
            
            foreach (var buyItemButton in _buyItemButtons)
            {
                buyItemButton.OnClick += BuyItem;
            }
        }

        private void BuyItem(BuyItemButton buyItemButton)
        {
            var typeItem = buyItemButton.TypeItem;
            if (_itemManager.BuyItem(typeItem))
            {
                _playerItemInstaller.SetItem(typeItem);
            }
        }

        private void SetPriceBuyButtons()
        {
            foreach (var buyItemButton in _buyItemButtons)
            {
                var price = _itemManager.GetPriceItemByTypeItem(buyItemButton.TypeItem);
                buyItemButton.SetPrice(price.Amount.ToString());
            }
        }

        private void ActivateBuyButton()
        {
            foreach (var buyItemButton in _buyItemButtons)
            {
                buyItemButton.SetActive(true);
            }
        }
        
        private void RefreshColorButton(Resource resource)
        {
            SetColorBuyButtons();
        }

        private void SetColorBuyButtons()
        {
            foreach (var buyItemButton in _buyItemButtons)
            {
                var state = _itemManager.CanBuyItem(buyItemButton.TypeItem)
                    ? ItemButtonState.Active
                    : ItemButtonState.Block;
                var color = _colorItemButtonProvider.GetColorByButtonState(state);
                buyItemButton.SetColor(color);
            }
        }

        private BuyItemButton GetBuyButtonByType(TypeItem typeItem)
        {
            return _buyItemButtons.FirstOrDefault(button => button.TypeItem == typeItem);
        }

        public void Dispose()
        {
            _resourceManagerLevel.OnResourceChange -= RefreshColorButton;
            
            foreach (var buyItemButton in _buyItemButtons)
            {
                buyItemButton.OnClick -= BuyItem;
            }
        }
        
        [Serializable]
        public class Settings
        {
            public List<BuyItemButton> BuyItemButtons = new List<BuyItemButton>();
        }
    }
}