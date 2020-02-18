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
    struct SpawnLocation : IJobForEachWithEntity<Authoring.SpawnLocation>
    {
        //static ConcurrentQueue<Spawn> spawnQueue = new ConcurrentQueue<Spawn>();

        //static IEnumerable<Spawn> spawns = new List<Spawn>();

        //static IEnumerable<Entity> spawnsTest = new List<Entity>();

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            //foreach (var spawn in spawnsTest)
            //{
            //    var spawnData = new Spawn
            //    {
            //        HasSpawned = true
            //    };

            //    dstManager.AddComponentData(entity, spawnData);
            //}
        }

        public void Execute(Entity entity, int index, ref System_Data.SpawnLocation spawnLocation, [ReadOnly] ref Authoring.Level level)
        {
            if (!level.InProgress)
            {
                return;
            }
            var spawns = spawnLocation.Start();
            //spawns.ForEach(spawn => spawnQueue.Enqueue(spawn));
            //foreach (var spawn in spawns)
            //{
            //    spawnQueue.Enqueue(spawn);
            //    CommandBuffer.Instantiate(index, spawn.Unit);
            //    CommandBuffer.AddComponent(index, entity, spawn);
            //}
        }

        public void Execute(Entity entity, int index, ref Authoring.SpawnLocation spawnLocation)
        {
            //var spawns = spawnLocation.Start();
        }
    }
}
