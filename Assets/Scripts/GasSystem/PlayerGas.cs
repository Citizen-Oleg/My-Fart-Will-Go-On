using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GasSystem
{
    public class PlayerGas : ExpandingGas
    {
        public List<ParticleSystem> RadiusParticleSystems => _radiusParticleSystems;
        
        [SerializeField]
        private List<ParticleSystem> _radiusParticleSystems = new List<ParticleSystem>();
        
        public void SetRadiusAction(float radius)
        {
            _endRadius = radius;
        }
    }
}
