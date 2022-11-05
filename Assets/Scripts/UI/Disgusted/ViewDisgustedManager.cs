using System;
using System.Collections.Generic;
using Level;
using PersonComponent;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI.Disgusted
{
    public class ViewDisgustedManager : IDisposable
    {
        private readonly Dictionary<Person, ViewDisgusted> _viewDisgusteds = new Dictionary<Person, ViewDisgusted>();
        
        private readonly List<Person> _persons;
        private readonly RectTransform _container;
        private readonly ViewDisgusted _prefabView;
        private readonly Camera _camera;
        
        public ViewDisgustedManager(LevelSettings levelSettings, Settings settings)
        {
            _camera = Camera.main;
            _persons = levelSettings.Persons;
            _container = settings.Container;
            _prefabView = settings.ViewDisgusted;

            foreach (var person in _persons)
            {
                CreateView(person);
                person.OnHide += HideView;
            }
        }

        private void CreateView(Person person)
        {
            var view = Object.Instantiate(_prefabView, _container);
            view.Initialize(_camera, person);
            _viewDisgusteds.Add(person, view);
        }

        private void HideView(Person person)
        {
            _viewDisgusteds[person].gameObject.SetActive(false);
            _viewDisgusteds.Remove(person);
        }
        
        public void Dispose()
        {
            foreach (var person in _persons)
            {
                person.OnHide -= HideView;
            }
        }

        [Serializable]
        public class Settings
        {
            public RectTransform Container;
            public ViewDisgusted ViewDisgusted;
        }
    }
}