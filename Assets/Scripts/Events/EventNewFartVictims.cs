using Emotion;
using GasSystem;
using PersonComponent;
using SimpleEventBus.Events;

namespace Events
{
    public class EventNewFartVictims : EventBase
    {
        public IEmotionPerson EmotionPerson { get; }
        public IResourceObject ResourceObject { get; }
        public EmotionFraction EmotionFraction { get; }
        public Gas Gas { get; }

        public EventNewFartVictims(IResourceObject resourceObject, IEmotionPerson emotionPerson, EmotionFraction emotionFraction, 
            Gas gas)
        {
            EmotionPerson = emotionPerson;
            EmotionFraction = emotionFraction;
            ResourceObject = resourceObject;
            Gas = gas;
        }
    }
}