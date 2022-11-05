using UnityEngine;

namespace Emotion
{
    public interface IEmotionPerson
    {
        public Transform UIPosition { get; }
        public Transform Transform { get; }
    }
}