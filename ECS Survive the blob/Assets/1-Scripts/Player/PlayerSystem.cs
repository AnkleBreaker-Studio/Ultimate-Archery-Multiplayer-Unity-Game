using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class PlayerSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        Entities.WithoutBurst().ForEach((ref Translation trans, ref Rotation rot, in PlayerData data) =>
        {
            int direction = 0;
            direction += Input.GetKey(data.left) ? -1 : 0;
            direction += Input.GetKey(data.right) ? 1 : 0;
            float3 newPos = new float3(trans.Value.x + direction * deltaTime * data.speed, trans.Value.y, trans.Value.z);
            if (newPos.x > -7 && newPos.x < 7)
            {
                trans.Value = newPos;
            }
            rot.Value = quaternion.identity;
        }).Run();
        return default;
    }
}
