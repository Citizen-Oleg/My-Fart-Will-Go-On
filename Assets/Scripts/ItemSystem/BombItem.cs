using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ItemSystem
{
    public class BombItem : Item
    {
        [SerializeField]
        private GameObject _modelBomb;
        [SerializeField]
        private float _lifeTime;
        
        public override async UniTaskVoid Activate()
        {
            _modelBomb.gameObject.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(_delayBeforeActivation)); 
            _gas.LetOffTheGas();
            _modelBomb.gameObject.SetActive(false);

            await UniTask.Delay(TimeSpan.FromSeconds(_lifeTime));
            _itemManager.ReleaseItem(this);
        }
    }
}