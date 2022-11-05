using UnityEngine;
using Zenject;

namespace PersonComponent
{
    public class PersonInstaller : MonoInstaller
    {
        [SerializeField]
        private Person _person;
        [SerializeField]
        private PersonMovementController.Settings _settingsPersonMovementController;

        public override void InstallBindings()
        {
            Container.BindInstance(_settingsPersonMovementController);
            Container.BindInstance(_person);
          
            Container.Bind<PersonMovementController>().AsSingle().NonLazy();
        }
    }
}