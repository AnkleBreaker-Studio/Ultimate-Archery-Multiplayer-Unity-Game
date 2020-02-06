using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class DetectGroundSystem : JobComponentSystem
{
    private BuildPhysicsWorld physicsWorld;

    protected override void OnCreate()
    {
        this.physicsWorld = this.World.GetExistingSystem<BuildPhysicsWorld>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        PhysicsWorld localPhysicsWorld = this.physicsWorld.PhysicsWorld;
        JobHandle handle = this.Entities.ForEach((ref JumpData jumpData, in Translation trans, in RenderBounds bounds) =>
        {
            float3 direction = new float3(0, -1, 0);
            float width = bounds.Value.Size.x - 0.05f;
            float length = bounds.Value.Size.y / 2 + 0.01f;
            CollisionFilter raycastFilter = new CollisionFilter()
            {
                BelongsTo = ~0u, // everything
                CollidesWith = 6, // 011
                GroupIndex = 0
            };

            // Trigger 3 raycast below the player and test if they collide to something and change the is on ground data

            RaycastInput inputLeft = new RaycastInput()
            {
                Start = trans.Value - new float3(width / 2, 0, 0),
                End = trans.Value - new float3(width / 2, 0, 0) + direction * length,
                Filter = raycastFilter
            };

            RaycastInput inputMid = new RaycastInput()
            {
                Start = trans.Value,
                End = trans.Value + direction * length,
                Filter = raycastFilter
            };

            RaycastInput inputRight = new RaycastInput()
            {
                Start = trans.Value + new float3(width / 2, 0, 0),
                End = trans.Value + new float3(width / 2, 0, 0) + direction * length,
                Filter = raycastFilter
            };
            if (localPhysicsWorld.CastRay(inputLeft, out Unity.Physics.RaycastHit hitLeft) || 
                localPhysicsWorld.CastRay(inputMid, out Unity.Physics.RaycastHit hitMid) || 
                localPhysicsWorld.CastRay(inputRight, out Unity.Physics.RaycastHit hitRight))
            {
                jumpData.IsOnGround = true;
            }
            else
            {
                jumpData.IsOnGround = false;
            }

            /*
            // To visualize ray and see easily if it touch the ground or not

            if (jumpData.IsOnGround)
            {
                Debug.DrawRay(inputLeft.Start, direction * length, Color.green);
                Debug.DrawRay(inputMid.Start, direction * length, Color.green);
                Debug.DrawRay(inputRight.Start, direction * length, Color.green);
            }
            else
            {
                Debug.DrawRay(inputLeft.Start, direction * length, Color.red);
                Debug.DrawRay(inputMid.Start, direction * length, Color.red);
                Debug.DrawRay(inputRight.Start, direction * length, Color.red);
            }
            */
        }).Schedule(inputDeps);
        
        return handle;
    }
}