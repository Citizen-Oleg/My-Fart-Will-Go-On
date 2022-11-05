using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Emotion
{
    public class EmotionFractionProvider
    {
        private readonly List<EmotionFractionType> _emotionFractions;
        
        public EmotionFractionProvider(Settings settings)
        {
            _emotionFractions = settings.EmotionFractions;
        }
        
        public EmotionType GetRandomEmotionTypeByEmotionFraction(EmotionFraction emotionFraction)
        {
            foreach (var emotionFractionType in _emotionFractions)
            {
                if (emotionFractionType.EmotionFraction == emotionFraction)
                {
                    var randomIndex = Random.Range(0, emotionFractionType.EmotionTypes.Count);
                    return emotionFractionType.EmotionTypes[randomIndex];
                }
            }

            return EmotionType.Angry_0;
        }
        
        [Serializable]
        public class Settings
        {
            public List<EmotionFractionType> EmotionFractions = new List<EmotionFractionType>();
        }

        [Serializable]
        public class EmotionFractionType
        {
            public EmotionFraction EmotionFraction;
            public List<EmotionType> EmotionTypes = new List<EmotionType>();
        }
    }
}