using System;
using UnityEngine;
using UnityTemplateProjects;

namespace SaveLoadSystem
{
    public class LoadController
    {
        private readonly Settings _settings;
    
        public LoadController(Settings settings)
        {
            _settings = settings;
        }

        public int GetLevel()
        {
            return PlayerPrefs.HasKey(GlobalConstants.LAST_LEVEL) ? PlayerPrefs.GetInt(GlobalConstants.LAST_LEVEL) : _settings.StartScene;
        }

        [Serializable]
        public class Settings
        {
            public int StartScene;
        }
    }
}