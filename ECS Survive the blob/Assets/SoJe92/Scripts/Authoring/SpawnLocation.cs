using Assets.SoJe92.Scripts.Authoring;
using ECS.Scripts.Contracts;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace ECS.Scripts.Authoring
{
    [System.Serializable]
    public struct SpawnLocation : IComponentData
    {
        public Entity entities;

        public int Limit;

        public int Spawned;

        public bool IsRandom;

        public bool IsSpawning;
    }
}
