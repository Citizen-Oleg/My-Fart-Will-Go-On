using System;
using Emotion;
using Events;
using Tools.SimpleEventBus;
using UnityEngine;

namespace PlayerComponent
{
    public class Player : MonoBehaviour, IEmotionPerson
    {
        public Transform UIPosition => _uiPosition;
        public Transform Transform => transform;
        public Transform FxPosition => _FXPosition;
        
        [SerializeField]
        private Transform _uiPosition;
        [SerializeField]
        private Transform _FXPosition;
        
        
        private IDisposable _subscription;

        private EventSatisfactionPlayerEmotion _eventSatisfactionPlayerEmotion;

        private void Awake()
        {
            _eventSatisfactionPlayerEmotion = new EventSatisfactionPlayerEmotion(this, EmotionFraction.Satisfaction);
            _subscription = EventStreams.UserInterface.Subscribe<EventNewFartVictims>(ActivateSatisfaction);
        }

        private void ActivateSatisfaction(EventNewFartVictims eventNewFartVictims)
        {
            EventStreams.UserInterface.Publish(_eventSatisfactionPlayerEmotion);
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }

        [Serializable]
        public class Settings
        {
            public float Speed;
            public Rigidbody Rigidbody;
            public Animator Animator;
        }
    }
}