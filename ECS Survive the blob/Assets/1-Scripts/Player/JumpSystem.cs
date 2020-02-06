using PoonGaloreECS;
using Unity.Physics.Authoring;
using Unity.Physics.Systems;
using Unity.Rendering;
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
    private BuildPhysicsWorld physicsWorld;

    protected override void OnCreate()
    {
        this.physicsWorld = this.World.GetExistingSystem<BuildPhysicsWorld>();
    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        this.Entities.ForEach((ref JumpData jumpData, ref PhysicsVelocity vel, ref Translation trans, ref PhysicsMass mass, in PlayerControlsInputData inputData, in RenderBounds renderer) =>
        {
            float3 direction = new float3(0, -1, 0);
            float width = renderer.Value.Size.x-0.1f;
            float length = renderer.Value.Size.y / 2 + 0.2f;
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
            if (this.physicsWorld.PhysicsWorld.CastRay(inputLeft, out Unity.Physics.RaycastHit hitLeft) || this.physicsWorld.PhysicsWorld.CastRay(inputMid, out Unity.Physics.RaycastHit hitMid) || this.physicsWorld.PhysicsWorld.CastRay(inputRight, out Unity.Physics.RaycastHit hitRight))
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