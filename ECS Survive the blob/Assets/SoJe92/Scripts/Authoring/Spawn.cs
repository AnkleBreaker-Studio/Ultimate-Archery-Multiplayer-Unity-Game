using ECS.Scripts.Contracts;
using Unity.Entities;
using UnityEngine;

namespace ECS.Scripts.Authoring
{
    [GenerateAuthoringComponent]
    struct Spawn : ISpawn, IComponentData, IUnit
    {
        public Entity Unit { get; set; }

        public bool HasSpawned { get; set; }

        public void Reset()
        {
            HasSpawned = false;
        }
    }
}
