using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace ECS.Scripts.Systems
{
    class Level : JobComponentSystem
    {
        [ReadOnly]
        public bool IsComplete;

        [BurstCompile]
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            Entities
                .ForEach((Authoring.Level level) =>
                {
                    if (level.IsComplete || level.Failed)
                    {
                        level.End();
                    }
                    else if (!level.InProgress)
                    {
                        level.Start();
                    }
                })
            .WithoutBurst()
            .Run();

            Entities
                .WithChangeFilter<Authoring.Spawn>()
                .ForEach((Authoring.Level level, Authoring.Spawn spawn) =>
                {
                    Debug.Log("Level with spawn");
                })
            .WithoutBurst()
            .Run();

            return default;
        }
    }
}
