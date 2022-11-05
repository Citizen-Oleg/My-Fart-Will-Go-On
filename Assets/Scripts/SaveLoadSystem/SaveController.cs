using System;
using ResourceSystem;
using Tools.SimpleEventBus;
using UnityEditor;
using UnityEngine;
using UnityTemplateProjects;

namespace SaveLoadSystem
{
    public class SaveController
    {
        public void SaveNumberLevel(int id)
        {
            PlayerPrefs.SetInt(GlobalConstants.LAST_LEVEL, id);
        }
        
#if UNITY_EDITOR
        [MenuItem("Tools/ClearSave")]
        public static void ClearSave()
        {
            PlayerPrefs.DeleteAll();
        }
#endif
    }
}