using System;
using Events;
using Level;
using Tools.SimpleEventBus;
using UnityEngine.UI;

namespace View
{
    public class LevelCompletionBar : IDisposable
    {
        private readonly IDisposable _subscription;
        private readonly Slider _slider;
        
        public LevelCompletionBar(Settings settings, LevelSettings levelSettings)
        {
            _slider = settings.Slider;
            _slider.maxValue = levelSettings.Persons.Count;
            _slider.minValue = 0;
            _slider.value = 0;

            _subscription = EventStreams.UserInterface.Subscribe<EventNewFartVictims>(ChangeValueSlider);
        }

        private void ChangeValueSlider(EventNewFartVictims eventNewFartVictims)
        {
            _slider.value++;
        }
        
        public void Dispose()
        {
            _subscription?.Dispose();
        }
        
        [Serializable]
        public class Settings
        {
            public Slider Slider;
        }
    }
}