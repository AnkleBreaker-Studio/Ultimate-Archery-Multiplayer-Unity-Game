using ECS.Scripts.Jobs;
using System.Collections.Concurrent;
using Unity.Entities;
using Unity.Jobs;

namespace ECS.Scripts.Systems
{
    public class Level : ComponentSystem
    {
        EntityCommandBufferSystem m_Barrier;

        protected override void OnCreate()
        {
            m_Barrier = World.GetOrCreateSystem<EntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var job = new Jobs.Level
            {
                CommandBuffer = m_Barrier.CreateCommandBuffer()
            }.ScheduleSingle(this);

            m_Barrier.AddJobHandleForProducer(job);
        }
    }
}