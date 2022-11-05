using System;
using PersonComponent;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Disgusted
{
    public class ViewDisgusted : MonoBehaviour
    {
        private AccumulatorDisgust _accumulatorDisgust;

        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private Image _fillSlider;
        [SerializeField]
        private Color _startColor;
        [SerializeField]
        private Color _endColor;
        
        private RectTransform _currentTransform;
        private RectTransform _container;
        private Transform _attachPoint;

        private Camera _camera;

        public void Initialize(Camera camera, Person person)
        {
            _camera = camera;
            _accumulatorDisgust = person.AccumulatorDisgust;
            
            _container = transform.parent as RectTransform;
            _currentTransform = transform as RectTransform;
            
            _attachPoint = person.UiDisgustPosition;
            _slider.value = _accumulatorDisgust.CurrentPatience;
            _slider.minValue = 0;
            _slider.maxValue = _accumulatorDisgust.MaxPatience;

            _accumulatorDisgust.OnChangePatience += RefreshInformation;
            gameObject.SetActive(false);
        }

        private void RefreshInformation()
        {
            gameObject.SetActive(true);
            _slider.value = _accumulatorDisgust.CurrentPatience;

            var color = _startColor;
            var t = _accumulatorDisgust.CurrentPatience / _accumulatorDisgust.MaxPatience;
            color = Color.Lerp(_startColor, _endColor, t);

            if (t <= 0.35f)
            {
                color.g = _startColor.g;
            }
            else
            {
                color.r = _endColor.r;
            }

            _fillSlider.color = color;
        }

        protected void LateUpdate()
        {
            if (_attachPoint != null)
            {
                _currentTransform.anchoredPosition = UIUtility.WorldToCanvasAnchoredPosition(_camera, _container, _attachPoint.position);
            }
        }

        private void OnDestroy()
        {
            _accumulatorDisgust.OnChangePatience -= RefreshInformation;
        }
    }
}