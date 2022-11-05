using System;
using Assets.Scripts.Managers.ScreensManager;
using Events;
using Managers.ScreensManager;
using ResourceSystem;
using Screens.VictoryScreenComponent;
using Tools.SimpleEventBus;

namespace Level
{
    public class LevelController : IDisposable
    {
        private readonly int _targetNumberOfDropouts;
        private readonly IDisposable _subscription;
        private readonly ScreenManager _screenManager;
        private readonly Resource _reward;

        private bool _isActivate;
        private int _currentCount;
        
        public LevelController(LevelSettings settings, ScreenManager screenManager)
        {
            _screenManager = screenManager;
            _reward = settings.Reward;
            _targetNumberOfDropouts = settings.Persons.Count;
            _subscription = EventStreams.UserInterface.Subscribe<EventNewFartVictims>(AddingAccount);
        }

        private void AddingAccount(EventNewFartVictims eventNewFartVictims)
        {
            if (++_currentCount >= _targetNumberOfDropouts && !_isActivate)
            {
                _isActivate = true;
                EventStreams.UserInterface.Publish(new EventVictory());
                var context = new ResultScreenContext(_reward.Amount);
                _screenManager.OpenScreenWithContext(ScreenType.VictoryScreen, context);
            }
        }
        
        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}