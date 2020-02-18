using ECS.Scripts.Contracts;
using Unity.Entities;
using UnityEngine;

namespace ECS.Scripts.Authoring
{
    public struct Spawn : ISpawn, IComponentData, IUnit
    {
        public Entity Unit;

        public bool HasSpawned;

        public void Reset()
        {
            HasSpawned = false;
        }

        public Entity Execute()
        {
            return Unit;
        }
    }
}
