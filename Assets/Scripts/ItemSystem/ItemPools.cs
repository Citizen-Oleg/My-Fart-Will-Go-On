using System;
using System.Collections.Generic;
using Events;
using Tools.SimpleEventBus;
using UnityEngine;

namespace ItemSystem
{
    public class ItemPools : IDisposable
    {
        private readonly Dictionary<TypeItem, MonoBehaviourPool<Item>> _monoBehaviourPools =
            new Dictionary<TypeItem, MonoBehaviourPool<Item>>();
        private readonly IDisposable _subscription;

        public ItemPools(Settings settings, Transform container)
        {
            _subscription = EventStreams.UserInterface.Subscribe<EventLoadNextLevel>(ReleaseAll);
            foreach (var settingsItem in settings.Items)
            {
                var pool = new MonoBehaviourPool<Item>(settingsItem, container, settings.CountPool);
                _monoBehaviourPools.Add(settingsItem.ItemType, pool);
            }
        }

        public Item TakeItemByTypeItem(TypeItem typeItem)
        {
            return _monoBehaviourPools[typeItem].Take();
        }

        public void ReleaseItem(Item item)
        {
            _monoBehaviourPools[item.ItemType].Release(item);
        }
        
        private void ReleaseAll(EventLoadNextLevel eventLoadNextLevel)
        {
            foreach (var monoBehaviourPool in _monoBehaviourPools)
            {
                monoBehaviourPool.Value.ReleaseAll();
            }
        }
        
        [Serializable]
        public class Settings
        {
            public int CountPool = 2;
            public List<Item> Items = new List<Item>();
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}