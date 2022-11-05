using Events;
using Tools.SimpleEventBus;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityTemplateProjects
{
    public class ButtonFart : MonoBehaviour, IPointerClickHandler
    {
        private readonly EventFart _eventFart = new EventFart();
        
        public void OnPointerClick(PointerEventData eventData)
        {
            EventStreams.UserInterface.Publish(_eventFart);
        }
    }
}