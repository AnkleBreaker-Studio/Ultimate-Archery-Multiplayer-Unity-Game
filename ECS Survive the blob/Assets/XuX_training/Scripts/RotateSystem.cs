using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace XuX_training.Scripts
{
    public class RotateSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            float deltaTime = Time.DeltaTime;

            var jobHandle = Entities.ForEach((ref RotationEulerXYZ euler, in Rotate rotate) =>
            {
                euler.Value.y += rotate.RadiansPerSecond * deltaTime;
            }).Schedule(inputDeps);

            return jobHandle;
        }
    }
}