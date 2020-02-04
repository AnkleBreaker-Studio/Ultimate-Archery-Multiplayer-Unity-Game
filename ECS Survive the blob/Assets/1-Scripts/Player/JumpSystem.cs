using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Transforms;
using UnityEngine;

public class JumpSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref JumpData jumpData, ref PhysicsVelocity vel, ref Translation trans, ref PhysicsMass mass) =>
            {
                if (math.abs(vel.Linear.y) < 0.1 && trans.Value.y < 2)
                {
                    jumpData.isOnGround = true;
                }

                if (Input.GetKeyDown(KeyCode.Space) && jumpData.isOnGround)
                {
                    jumpData.isOnGround = false;
                    vel.ApplyLinearImpulse(mass, new float3(0, jumpData.jumpPower, 0));
                }
            });
    }
}
