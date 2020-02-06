using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Transforms;
using UnityEngine;

public class BlobAIsyste : JobComponentSystem
{
    private EntityQuery _playerQuery;
    
    protected override void OnCreate()
    {
        EntityQueryDesc queryDesc = new EntityQueryDesc()
        {
            All = new ComponentType[] {typeof(Translation), typeof(PoonGaloreECS.PlayerData)}
        };
        
        this._playerQuery = GetEntityQuery(queryDesc);
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        var players = this._playerQuery.ToComponentDataArray<Translation>(Allocator.TempJob);
        //Debug.Log(players);
        float3 playerPosition = players[0].Value;
        JobHandle handle = this.Entities.ForEach((ref Translation trans, ref PhysicsVelocity vel, ref Rotation rot, in PhysicsMass mass, in BlobData blob, in JumpData jumpData) =>
        {
            float direction = playerPosition.x < trans.Value.x ? -1 : 1;
            trans.Value += new float3(direction * deltaTime * blob.Speed, 0, 0);
            if (jumpData.IsOnGround)
            {
                vel.ApplyLinearImpulse(mass, new float3(0, jumpData.JumpPower, 0));
            }
            rot.Value = quaternion.identity;
        }).Schedule(inputDeps);


        players.Dispose();
        return handle;
    }
    
}
