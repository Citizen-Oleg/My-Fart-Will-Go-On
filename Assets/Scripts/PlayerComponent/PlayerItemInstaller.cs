using System;
using GasSystem;
using ItemSystem;
using UnityEngine;

namespace PlayerComponent
{
    public class PlayerItemInstaller
    {
        private readonly Transform _positionItem;
        private readonly ItemManager _itemManager;
        private readonly PlayerGasAdjuster _playerGasAdjuster;

        public PlayerItemInstaller(Settings settings, ItemManager itemManager, PlayerGasAdjuster playerGasAdjuster)
        {
            _itemManager = itemManager;
            _playerGasAdjuster = playerGasAdjuster;
            _positionItem = settings.PositionItem;
        }

        public void SetItem(TypeItem typeItem)
        {
            var item = _itemManager.TakeItemByTypeItem(typeItem);
            item.transform.position = _positionItem.position;
            item.transform.rotation = _positionItem.rotation;
            item.Gas.Level = _playerGasAdjuster.IntensityImprove.InformationImprove.Level;
            item.Activate();
        }

        [Serializable]
        public class Settings
        {
            public Transform PositionItem;
        }
    }
}