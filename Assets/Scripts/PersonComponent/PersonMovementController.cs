using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace PersonComponent
{
    public class PersonMovementController
    {
        private readonly NavMeshAgent _navMeshAgent;

        public PersonMovementController(Settings settings)
        {
            _navMeshAgent = settings.NavMeshAgent;
            _navMeshAgent.speed = Random.Range(settings.MinSpeed, settings.MaxSpeed);
        }

        public void MoveToPosition(Vector3 position)
        {
            _navMeshAgent.destination = position;
        }

        [Serializable]
        public class Settings
        {
            public float MinSpeed;
            public float MaxSpeed;
            public NavMeshAgent NavMeshAgent;
        }
    }
}