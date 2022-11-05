using Cysharp.Threading.Tasks;
using GasSystem;
using ResourceSystem;
using UnityEngine;

namespace ItemSystem
{
    public abstract class Item : MonoBehaviour
    {
        public TypeItem ItemType => _itemType;
        public TypeGas UsingTypeGas => _usingTypeGas;
        public Transform AnchorPosition => _anchorPosition;
        public Resource Price => _price;
        public Gas Gas => _gas;
        
        [SerializeField]
        private TypeItem _itemType;
        [SerializeField]
        private TypeGas _usingTypeGas;
        [SerializeField]
        private Resource _price;

        [SerializeField]
        protected float _delayBeforeActivation;
        [SerializeField]
        protected Transform _anchorPosition;

        protected Gas _gas;
        protected ItemManager _itemManager;

        public virtual void Initialize(Gas gas, ItemManager itemManager)
        {
            _gas = gas;
            _itemManager = itemManager;
        }
        
        public abstract UniTaskVoid Activate();
    }
}