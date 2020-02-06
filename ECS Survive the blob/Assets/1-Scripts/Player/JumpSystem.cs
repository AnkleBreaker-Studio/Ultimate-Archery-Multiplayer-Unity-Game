using PoonGaloreECS;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class JumpSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        this.Entities.ForEach((ref JumpData jumpData, ref PhysicsVelocity vel, in PhysicsMass mass, in PlayerControlsInputData inputData) =>
        {
            // Make the player jump if he press jump key and if he is on the ground
            if (!Input.GetKeyDown(inputData.Jump) || !jumpData.IsOnGround)
            {
                return;
            }

            jumpData.IsOnGround = false;
            vel.ApplyLinearImpulse(mass, new float3(0, jumpData.JumpPower, 0));
        }).WithoutBurst().Run();

        return default;
    }
}