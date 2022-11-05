using System.Collections.Generic;
using PersonComponent;
using UnityEngine;

namespace GasSystem
{
    public static class RadiusPersonProvider
    {
        private const int DEFAULT_SIZE = 20;
        
        private static readonly List<Person> _persons = new List<Person>();
        private static readonly Collider[] _colliders = new Collider[DEFAULT_SIZE];

        public static List<Person> GetPerson(Transform transform, float radius, LayerMask layerMask)
        {
            if (_persons.Count != 0)
            {
                _persons.Clear();
            }
            
            var count = Physics.OverlapSphereNonAlloc(transform.position, radius, _colliders);

            if (count == 0)
            {
                return _persons;
            }
            
            for (var i = 0; i < count; i++)
            {
                if (_colliders[i] == null)
                {
                    break;
                }
                if (_colliders[i].TryGetComponent(out Person person))
                {
                    _persons.Add(person);
                }
            }

            return _persons;
        }
    }
}