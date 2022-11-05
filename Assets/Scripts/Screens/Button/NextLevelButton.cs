using Events;
using Tools.SimpleEventBus;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Screens.Button
{
    public class NextLevelButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            EventStreams.UserInterface.Publish(new EventLoadNextLevel());
        }
    }
}