using System;
using System.Diagnostics.Contracts;
using Emotion;
using Events;
using GasSystem;
using Level;
using ResourceSystem;
using Tools.SimpleEventBus;
using UnityEngine;
using Zenject;

namespace PersonComponent
{
    public class Person : MonoBehaviour, IEmotionPerson, IResourceObject
    {
        public event Action<Person> OnHide;
        
        public Resource Resource => _resource;
        public Transform UIPosition => _uiPosition;
        public Transform Transform => transform;
        public AccumulatorDisgust AccumulatorDisgust => _accumulatorDisgust;
        public Transform UiDisgustPosition => _uiDisgustPosition;
        
        [SerializeField]
        private Resource _resource;
        [SerializeField]
        private Transform _uiPosition;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private Transform _uiDisgustPosition;
        [SerializeField]
        private int _minimumPatience;
        [SerializeField]
        private int _maximumPatience;

        private Vector3 _positionBase;
        private PersonMovementController _personMovementController;
        private PersonAnimationController _personAnimationController;
        private AccumulatorDisgust _accumulatorDisgust;

        private bool _isGasSacrifice;

        private void Awake()
        {
            _accumulatorDisgust = new AccumulatorDisgust(_minimumPatience, _maximumPatience);
        }

        [Inject]
        private void Constructor(PersonMovementController personMovementController, LevelSettings levelSettings)
        {
            _personAnimationController = new PersonAnimationController(_animator);
            _positionBase = levelSettings.Street.position;
            _personMovementController = personMovementController;

            _accumulatorDisgust.OnLimitPatience += RunAwayFromGas;
        }

        public void InfluenceGas(Gas gas)
        {
            if (!_isGasSacrifice)
            {
                _accumulatorDisgust.Accumulator(gas);
            }
        }

        private void RunAwayFromGas(Gas gas)
        {
            if (!_isGasSacrifice)
            {
                _isGasSacrifice = true;
                _personMovementController.MoveToPosition(_positionBase);
                _personAnimationController.SetRun(true);
                var eventVictims = new EventNewFartVictims(this, this, EmotionFraction.Disgust, gas);
                EventStreams.UserInterface.Publish(eventVictims);
            }
        }

        public void Hide()
        {
            OnHide?.Invoke(this);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (_accumulatorDisgust != null)
            {
                _accumulatorDisgust.OnLimitPatience -= RunAwayFromGas;
            }
        }
    }
}