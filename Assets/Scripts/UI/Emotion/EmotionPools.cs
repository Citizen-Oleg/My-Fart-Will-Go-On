using System;
using System.Collections.Generic;
using Emotion;
using Events;
using Tools.SimpleEventBus;
using UnityEngine;

namespace UI.Emotion
{
    public class EmotionPools : IDisposable
    {
        private readonly Dictionary<EmotionFraction, Dictionary<EmotionType, MonoBehaviourPool<EmotionView>>>
            _emotionFractionDictionary = new Dictionary<EmotionFraction, Dictionary<EmotionType, MonoBehaviourPool<EmotionView>>>();
        private readonly IDisposable _subscription;

        private readonly EmotionFractionProvider _emotionFractionProvider;

        public EmotionPools(Settings settings, EmotionFractionProvider emotionFractionProvider, Transform container)
        {
            _subscription = EventStreams.UserInterface.Subscribe<EventLoadNextLevel>(ReleaseAll);
            _emotionFractionProvider = emotionFractionProvider;
            
            foreach (var emotionPrefab in settings.EmotionPrefabs)
            {
                var pool = new MonoBehaviourPool<EmotionView>(emotionPrefab, container.transform, settings.CountPool);

                var fraction = emotionPrefab.EmotionFraction;
                if (_emotionFractionDictionary.ContainsKey(fraction))
                {
                   _emotionFractionDictionary[fraction].Add(emotionPrefab.EmotionType, pool);
                }
                else
                {
                    var emotionTypeDictionary = new Dictionary<EmotionType, MonoBehaviourPool<EmotionView>>();
                    emotionTypeDictionary.Add(emotionPrefab.EmotionType, pool);
                    _emotionFractionDictionary.Add(fraction, emotionTypeDictionary);
                }
            }
        }

        public EmotionView GetRandomEmotionViewByEmotionFraction(EmotionFraction emotionFraction)
        {
            var randomEmotionType = _emotionFractionProvider.GetRandomEmotionTypeByEmotionFraction(emotionFraction);
            return _emotionFractionDictionary[emotionFraction][randomEmotionType].Take();
        }

        public void ReleaseView(EmotionView emotionView)
        {
            _emotionFractionDictionary[emotionView.EmotionFraction][emotionView.EmotionType].Release(emotionView);
        }
        
        private void ReleaseAll(EventLoadNextLevel eventLoadNextLevel)
        {
            foreach (var monoBehaviourPool in _emotionFractionDictionary)
            {
                foreach (var keyValuePair in monoBehaviourPool.Value)
                {
                    keyValuePair.Value.ReleaseAll();
                }
            }
        }
        
        [Serializable]
        public class Settings
        {
            public int CountPool = 3;
            public List<EmotionView> EmotionPrefabs = new List<EmotionView>();
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}