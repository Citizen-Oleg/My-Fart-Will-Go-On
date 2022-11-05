using System;
using System.Collections.Generic;
using Events;
using Tools.SimpleEventBus;
using UI.Emotion;
using UniRx;
using UnityEngine;

namespace Emotion
{
    public class EmotionManager : IDisposable
    {
        private Camera _camera;
        private readonly EmotionPools _emotionPools;
        private readonly CompositeDisposable _subscription;

        private readonly Dictionary<IEmotionPerson, EmotionView> _emotionViews = new Dictionary<IEmotionPerson, EmotionView>();

        public EmotionManager(EmotionPools emotionPools)
        {
            _camera = Camera.main;
            _emotionPools = emotionPools;
            _subscription = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<EventNewFartVictims>(CreateEmotionView),
                EventStreams.UserInterface.Subscribe<EventSatisfactionPlayerEmotion>(CreateEmotionView),
                EventStreams.UserInterface.Subscribe<EventNewLevelDownloadCompleted>(eventNewLevel => _camera = Camera.main)
            };
        }

        private void CreateEmotionView(IEmotionPerson emotionPerson, EmotionFraction emotionFraction)
        {
            if (_emotionViews.ContainsKey(emotionPerson))
            {
                if (_emotionViews[emotionPerson].isActiveAndEnabled)
                {
                    return;
                }
                
                DestroyView(emotionPerson);
            }
            
            var emotionView = _emotionPools.GetRandomEmotionViewByEmotionFraction(emotionFraction);
            emotionView.Initialize(emotionPerson, _camera);
           
            _emotionViews.Add(emotionPerson, emotionView);
            emotionView.OnHide += DestroyView;
        }
        
        private void DestroyView(IEmotionPerson emotionPerson)
        {
            if (_emotionViews.ContainsKey(emotionPerson))
            {
                _emotionViews[emotionPerson].OnHide -= DestroyView;
                _emotionPools.ReleaseView(_emotionViews[emotionPerson]);
                _emotionViews.Remove(emotionPerson);
            }
        }

        private void CreateEmotionView(EventNewFartVictims eventNewFartVictims)
        {
            CreateEmotionView(eventNewFartVictims.EmotionPerson, eventNewFartVictims.EmotionFraction);
        }
        
        private void CreateEmotionView(EventSatisfactionPlayerEmotion eventSatisfactionPlayerEmotion)
        {
            CreateEmotionView(eventSatisfactionPlayerEmotion.EmotionPerson, eventSatisfactionPlayerEmotion.EmotionFraction);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
            foreach (var keyValuePair in _emotionViews)
            {
                keyValuePair.Value.OnHide -= DestroyView;
            }
        }
    }
}