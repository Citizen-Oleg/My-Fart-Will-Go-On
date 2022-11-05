using System;
using Cysharp.Threading.Tasks;
using UnityEngine;


namespace Emotion
{
    [RequireComponent(typeof(ParticleSystem))]
    public class EmotionView : MonoBehaviour
    {
        public event Action<IEmotionPerson> OnHide;

        public EmotionFraction EmotionFraction => _emotionFraction;
        public EmotionType EmotionType => _emotionType;

        [SerializeField]
        private EmotionFraction _emotionFraction;
        [SerializeField]
        private EmotionType _emotionType;
      
        private ParticleSystem _particleSystem;
        private Transform _attachPoint;
        private Camera _camera;
        private IEmotionPerson _emotionPerson;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public void Initialize(IEmotionPerson emotionPerson, Camera camera)
        {
            _camera = camera;
            _emotionPerson = emotionPerson;
            _attachPoint = _emotionPerson.UIPosition;
            _particleSystem.Play();
            Countdown();
        }

        private async UniTaskVoid Countdown()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_particleSystem.main.duration));
            OnHide?.Invoke(_emotionPerson);
        }

        protected void LateUpdate()
        {
            if (_attachPoint != null)
            {
                transform.position = _emotionPerson.UIPosition.position;
                transform.LookAt(_camera.transform);
            }
        }
    }
}