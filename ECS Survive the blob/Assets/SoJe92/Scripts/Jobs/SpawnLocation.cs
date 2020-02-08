using ECS.Scripts.Authoring;
using ECS.Scripts.Contracts;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace ECS.Scripts.Jobs
{
    [BurstCompile]
    struct SpawnLocation : IJobForEachWithEntity<Authoring.SpawnLocation, Authoring.Level>
    {
        static ConcurrentQueue<GameObject> spawnQueue = new ConcurrentQueue<GameObject>();

        static IEnumerable<GameObject> spawns = new List<GameObject>();

        [WriteOnly]
        public EntityCommandBuffer.Concurrent CommandBuffer;

        public void Execute(Entity entity, int index, ref Authoring.SpawnLocation spawnLocation, [ReadOnly] ref Authoring.Level level)
        {
            if (!level.InProgress)
            {
                return;
            }
            spawns = spawnLocation.Start();
            foreach (var spawn in spawns)
            {
                spawnQueue.Enqueue(spawn);
                //CommandBuffer.AddComponent(index, entity, spawn);
            }
        }
    }
}
