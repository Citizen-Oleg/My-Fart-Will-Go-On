using System;
using Events;
using Tools.SimpleEventBus;
using UnityEngine;
using Zenject;

namespace PlayerComponent
{
    [RequireComponent(typeof(Collider))]
    public class PlayerPerceptionTriggerZone : MonoBehaviour
    {
        [Inject]
        private ExhaustGasController _exhaustGasController;

        private IDisposable _disposable;
        
        private void Awake()
        {
            _disposable = EventStreams.UserInterface.Subscribe<EventFart>(Fart);
            GetComponent<Collider>().isTrigger = true;
        }

        private void Fart(EventFart eventFart)
        {
            _exhaustGasController.ManualGasActivation();
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }

        public void ActivateClickAction()
        {
            _exhaustGasController.ManualGasActivation();
        }
    }
}