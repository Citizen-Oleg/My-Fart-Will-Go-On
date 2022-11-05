using System;
using System.Collections.Generic;
using PersonComponent;
using ResourceSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level
{
    [Serializable]
    public class LevelSettings
    {
        public Transform Street;
        public Resource Reward;
        public List<Person> Persons = new List<Person>();
    }
}