using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Transforms;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class JumpSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.ForEach((ref JumpData jumpData, ref PhysicsVelocity vel, ref Translation trans, ref PhysicsMass mass, in PlayerControlsInputData inputData) =>
            {
                if (math.abs(vel.Linear.y) < 0.1 && trans.Value.y < 2)
                {
                    jumpData.IsOnGround = true;
                }

                if (!Input.GetKeyDown(inputData.Jump) || !jumpData.IsOnGround) return;
                
                jumpData.IsOnGround = false;
                vel.ApplyLinearImpulse(mass, new float3(0, jumpData.jumpPower, 0));
                
            }).WithoutBurst().Run();

        return default;
    }
}
