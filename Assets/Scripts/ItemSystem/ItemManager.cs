using System.Collections.Generic;
using System.Linq;
using GasSystem;
using ResourceSystem;

namespace ItemSystem
{
    public class ItemManager
    {
        private readonly ResourceManagerLevel _resourceManagerLevel;
        private readonly ItemPools _itemPools;
        private readonly GasPools _gasPools;
        private readonly List<Item> _items;

        public ItemManager(ItemPools.Settings itemPoolsSettings, ItemPools itemPools, GasPools gasPools, ResourceManagerLevel resourceManagerLevel)
        {
            _itemPools = itemPools;
            _gasPools = gasPools;
            _resourceManagerLevel = resourceManagerLevel;
            _items = itemPoolsSettings.Items;
        }

        public Resource GetPriceItemByTypeItem(TypeItem typeItem)
        {
            return _items.FirstOrDefault(item => item.ItemType == typeItem).Price;
        }

        public bool CanBuyItem(TypeItem typeItem)
        {
            var price = GetPriceItemByTypeItem(typeItem);
            return _resourceManagerLevel.HasEnough(price);
        }

        public bool BuyItem(TypeItem typeItem)
        {
            if (CanBuyItem(typeItem))
            {
                _resourceManagerLevel.Pay(GetPriceItemByTypeItem(typeItem));
                return true;
            }

            return false;
        }

        public Item TakeItemByTypeItem(TypeItem typeItem)
        {
            var item = _itemPools.TakeItemByTypeItem(typeItem);

            var gas = _gasPools.GetGasByTypeGas(item.UsingTypeGas);
            gas.transform.parent = item.AnchorPosition;
            gas.transform.position = item.AnchorPosition.position;

            item.Initialize(gas, this);
            return item;
        }

        public void ReleaseItem(Item item)
        {
            _itemPools.ReleaseItem(item);
        }
    }
}