using System;
using ResourceSystem;

namespace UpgradeSystem.Upgraders
{
    public abstract class Upgrader
    {
        public event Action<Upgrader> OnImprove;

        public TypeUpgrade TypeUpgrade { get; protected set; }

        protected readonly ResourceManagerLevel _resourceManagerLevel;
        
        protected int _currentLevel = 1;
        
        public Upgrader(ResourceManagerLevel resourceManagerLevel)
        {
            _resourceManagerLevel = resourceManagerLevel;
        }

        public abstract bool IsMaxImprove();
        public abstract bool CanImprove();

        public virtual void Improve()
        {
            var nextImprove = GetInformationAboutTheNextImprovement();
            _resourceManagerLevel.Pay(nextImprove.Resource);
            _currentLevel++;
            OnImprove?.Invoke(this);
        }
        
        public abstract InformationImprove GetInformationAboutTheCurrentImprovement();
        public abstract InformationImprove GetInformationAboutTheNextImprovement();
        
        [Serializable]
        public class InformationImprove
        {
            public int Level;
            public Resource Resource;
        }
    }
}