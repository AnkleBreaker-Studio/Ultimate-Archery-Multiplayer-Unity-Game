using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

/// <summary>
/// JobComponentSystem combines the ComponentSystem and is used for both single and multi-threaded code by utilizing Run() for main or Schedule(inputDeps) for multi.
/// 
/// [AlwaysSynchronizeSystem] can be applied to a JobComponentSystem to force it to synchronize on all of its
/// dependencies before every update.  This attribute should only be applied when a synchronization point is
/// necessary every frame.
/// 
/// You must run the system on a main thread if you are utilizing main thread namespace such as UnityEngine.
/// 
/// To schedule multi threaded code you must return the job dependency (inputDeps) and call .Schedule(inputDeps).
/// </summary>
[AlwaysSynchronizeSystem]
public class PlayerMovementSystem : JobComponentSystem 
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var deltaTime = Time.DeltaTime;
        
        Entities.ForEach((ref PhysicsVelocity velocity, ref PhysicsMass mass,/*ref Translation trans,*/ ref Rotation rot, in PlayerData data, in PlayerControlsInputData inputData) =>
        {
            var direction = 0f;
            
            direction += Input.GetKey(inputData.MoveLeft) ? (-10 * data.Speed * deltaTime) : 0f;
            direction += Input.GetKey(inputData.MoveRight) ? (10 * data.Speed * deltaTime) : 0f;

            velocity.Linear.x = math.clamp(velocity.Linear.x, -data.Speed, data.Speed);
            
            //velocity.Linear.x = direction;
            velocity.ApplyLinearImpulse(mass, new float3(direction,0f,0f));

            /*float3 newPos = new float3(trans.Value.x + direction * deltaTime * data.Speed, trans.Value.y, trans.Value.z);
            if (newPos.x > -7 && newPos.x < 7)
            {
                trans.Value = newPos;
            }*/
            
            rot.Value = quaternion.identity; //keeps the player from rotating over
            
        }).WithoutBurst().Run();
        
        return default; 
    }
}
