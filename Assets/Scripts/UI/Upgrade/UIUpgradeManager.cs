using System;
using System.Collections.Generic;
using ResourceSystem;
using UI.Upgrade;

namespace UpgradeSystem.UI
{
    public class UIUpgradeManager : IDisposable
    {
        private readonly List<UpgradeButton> _upgradeButtons;
        private readonly UpgradeManager _upgradeManager;
        private readonly ResourceManagerLevel _resourceManagerLevel;
        private readonly ColorUpgradeButtonProvider _colorStateButtonProvider;
        
        public UIUpgradeManager(Settings settings, UpgradeManager upgradeController, ResourceManagerLevel resourceManagerLevel, 
            ColorUpgradeButtonProvider colorStateButtonProvider)
        {
            _upgradeButtons = settings.UpgradeButtons;
            _upgradeManager = upgradeController;
            _resourceManagerLevel = resourceManagerLevel;
            _colorStateButtonProvider = colorStateButtonProvider;
            _resourceManagerLevel.OnResourceChange += RefreshColorButton;
            
            foreach (var upgradeButton in _upgradeButtons)
            {
                upgradeButton.OnClick += BuyUpgrade;
            }
            
            RefreshColorButton();
            RefreshText();
        }

        private void BuyUpgrade(UpgradeButton upgradeButton)
        {
            var typeUpgrade = upgradeButton.TypeUpgrade;
            if (_upgradeManager.Improve(typeUpgrade))
            {
                var currentInformation = _upgradeManager.GetCurrentInformationImprove(typeUpgrade);
                upgradeButton.SetLevel(currentInformation.Level);
                
                if (_upgradeManager.IsMaxImprove(typeUpgrade))
                {
                    upgradeButton.IsMax = true;
                    upgradeButton.ChangeColor(_colorStateButtonProvider.GetColorByStateButton(StateUpgradeButton.Max));
                    upgradeButton.SetPriceText("Max");
                    return;
                }

                var nextImprove = _upgradeManager.GetNextInformationImprove(typeUpgrade);
                upgradeButton.SetPriceText(nextImprove.Resource.Amount.ToString());
                RefreshColorButton();
            }
        }

        private void RefreshColorButton(Resource resource)
        {
            RefreshColorButton();
        }

        private void RefreshColorButton()
        {
            foreach (var upgradeButton in _upgradeButtons)
            {
                if (upgradeButton.IsMax)
                {
                    continue;
                }

                var canImprove =  _upgradeManager.CanImprove(upgradeButton.TypeUpgrade);
                var color = _colorStateButtonProvider.GetColorByStateButton(canImprove
                    ? StateUpgradeButton.Active
                    : StateUpgradeButton.Block);
                
                upgradeButton.ChangeColor(color);
            }
        }

        private void RefreshText()
        {
            foreach (var upgradeButton in _upgradeButtons)
            {
                var nextImprove = _upgradeManager.GetNextInformationImprove(upgradeButton.TypeUpgrade);
                upgradeButton.SetPriceText(nextImprove == null ? "Max" : nextImprove.Resource.Amount.ToString());

                var currentImprove = _upgradeManager.GetCurrentInformationImprove(upgradeButton.TypeUpgrade);
                upgradeButton.SetLevel(currentImprove.Level);
            }
        }
        
        public void Dispose()
        {
            _resourceManagerLevel.OnResourceChange -= RefreshColorButton;
            
            foreach (var upgradeButton in _upgradeButtons)
            {
                upgradeButton.OnClick -= BuyUpgrade;
            }
        }

        [Serializable]
        public class Settings
        {
            public List<UpgradeButton> UpgradeButtons = new List<UpgradeButton>();
        }
    }
}