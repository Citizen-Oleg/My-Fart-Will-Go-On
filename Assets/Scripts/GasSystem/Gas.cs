using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GasSystem
{
    [RequireComponent(typeof(ParticleSystem))]
    public abstract class Gas : MonoBehaviour
    {
        public int Level { get; set; }
        
        public TypeGas TypeGas => _typeGas;
        public float PowerStink => _powerStink;

        [SerializeField]
        private TypeGas _typeGas;
        [SerializeField]
        private float _powerStink;

        protected ParticleSystem _particleSystem;
        protected GasPools _gasPools;
        

        protected virtual void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }
        
        public void Initialize(GasPools gasPools)
        {
            _gasPools = gasPools;
        }

        public abstract UniTaskVoid LetOffTheGas();
        protected abstract void ResetSettings();

        private void OnDisable()
        {
            ResetSettings();
        }
    }
}