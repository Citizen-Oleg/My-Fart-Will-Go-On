using System;
using Cysharp.Threading.Tasks;
using PlayerComponent;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.View
{
    public class ViewTimer : IDisposable, ITickable
    {
        private readonly Image _image;
        private readonly ExhaustGasController _exhaustGasController;
        private readonly Camera _camera;

        public ViewTimer(Settings settings, ExhaustGasController exhaustGasController)
        {
            _exhaustGasController = exhaustGasController;
            _image = settings.Image;
            _camera = Camera.main;

            _exhaustGasController.OnRefreshTimer += StartTimer;
        }

        private void StartTimer(float cooldown)
        {
            StartAsyncTimer(cooldown).Forget();
        }

        private async UniTaskVoid StartAsyncTimer(float cooldown)
        {
            _image.fillAmount = 0;
            var startTime = 0f;

            while (cooldown >= startTime && _image != null)
            {
                startTime += Time.deltaTime;
                _image.fillAmount += Time.deltaTime / cooldown;

                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }
        
        public void Dispose()
        {
            _exhaustGasController.OnRefreshTimer -= StartTimer;
        }
        
        [Serializable]
        public class Settings
        {
            public Image Image;
        }

        public void Tick()
        {
            _image.transform.LookAt(_camera.transform, -Vector3.back);
        }
    }
}