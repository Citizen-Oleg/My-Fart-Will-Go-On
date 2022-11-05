using Emotion;
using PersonComponent;
using SimpleEventBus.Events;

namespace Events
{
    public class EventSatisfactionPlayerEmotion : EventBase
    {
        public IEmotionPerson EmotionPerson { get; }
        public EmotionFraction EmotionFraction { get; }

        public EventSatisfactionPlayerEmotion(IEmotionPerson emotionPerson, EmotionFraction emotionFraction)
        {
            EmotionPerson = emotionPerson;
            EmotionFraction = emotionFraction;
        }
    }
}