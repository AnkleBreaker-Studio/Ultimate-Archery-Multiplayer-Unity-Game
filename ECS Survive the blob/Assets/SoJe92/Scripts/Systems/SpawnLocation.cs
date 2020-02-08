using ECS.Scripts.Authoring;
using ECS.Scripts.Contracts;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace ECS.Scripts.Systems
{
    public class SpawnLocation : JobComponentSystem
    {
        static ConcurrentQueue<ISpawn> spawnQueue = new ConcurrentQueue<ISpawn>();

        static IEnumerable<Spawn> spawns = new List<Spawn>();

        [WriteOnly]
        public EntityCommandBuffer.Concurrent CommandBuffer;

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new Jobs.SpawnLocation()
            {
                CommandBuffer = CommandBuffer
            }.ScheduleSingle(this, inputDeps);

            return job;
        }
    }
}
