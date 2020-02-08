using Unity.Collections;
using Unity.Entities;

namespace ECS.Scripts.Jobs
{
    struct Level : IJobForEachWithEntity<Authoring.Level>
    {
        [WriteOnly]
        public EntityCommandBuffer CommandBuffer;

        public void Execute(Entity entity, int index, ref Authoring.Level level)
        {
            if (!level.IsComplete && !level.InProgress)
            {
                Start(level);
            }
            if(level.Failed && level.InProgress)
            {
                End(level);
            }
        }

        public void Start(Authoring.Level level)
        {
            level.IsComplete = false;
            level.InProgress = true;
        }

        public void End(Authoring.Level level)
        {
            level.IsComplete = true;
            level.InProgress = false;
        }
    }
}
