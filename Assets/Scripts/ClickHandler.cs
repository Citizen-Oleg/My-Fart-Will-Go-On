using System;
using PlayerComponent;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ClickHandler : ITickable
{
    private readonly Camera _camera;
    private readonly LayerMask _layerMask;
        
    public ClickHandler(Settings settings)
    {
        _layerMask = settings.LayerMask;
        _camera = Camera.main;
    }
        
    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
         //   CheckClick(); 
        }
    }

    private void CheckClick()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, float.MaxValue, _layerMask))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out PlayerPerceptionTriggerZone playerPerceptionTriggerZone))
            {
                playerPerceptionTriggerZone.ActivateClickAction();
            }
        }
    }

    [Serializable]
    public class Settings
    {
        public LayerMask LayerMask;
    }
}