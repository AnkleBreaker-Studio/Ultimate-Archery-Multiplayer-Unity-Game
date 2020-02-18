using ECS.Scripts.Authoring;
using ECS.Scripts.Contracts;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace ECS.Scripts.Systems
{
    public class Spawn : JobComponentSystem
    {
        private EntityCommandBufferSystem commandBufferSystem;

        protected override void OnCreate()
        {
            commandBufferSystem = World
                .DefaultGameObjectInjectionWorld
                .GetOrCreateSystem<EntityCommandBufferSystem>();
        }

        [BurstCompile]
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            EntityCommandBuffer.Concurrent commandBufferCreate
                = commandBufferSystem.CreateCommandBuffer().ToConcurrent();

            Entities
                .ForEach((int entityInQueryIndex, Authoring.Spawn spawn, in LocalToWorld center) =>
            {
                UnityEngine.Debug.Log("Bla bla");

                if (spawn.HasSpawned)
                {
                    return;
                }

                Entity spawnedEntity = commandBufferCreate
                    .Instantiate(entityInQueryIndex,
                                 spawn.Unit);

                commandBufferCreate.SetComponent(entityInQueryIndex,
                                                 spawnedEntity,
                                                 center);
                commandBufferCreate.AddComponent(entityInQueryIndex,
                                                 spawnedEntity,
                                                 spawn);
            })
            .WithoutBurst()
            .Run();

            return default;
            //commandBufferSystem.AddJobHandleForProducer(createJobHandle);

            //return createJobHandle;
            //var job = new Jobs.SpawnLocation().Run(this, inputDeps);

            //return job;
        }
    }
}
