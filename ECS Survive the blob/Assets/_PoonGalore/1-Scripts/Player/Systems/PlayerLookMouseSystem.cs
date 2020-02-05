namespace PoonGaloreECS
{
    using Unity.Entities;
    using Unity.Jobs;
    using Unity.Mathematics;
    using Unity.Transforms;
    using UnityEngine;

    [AlwaysSynchronizeSystem]
    public class PlayerLookMouseSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            Entities.ForEach((ref Rotation rotation, ref Translation trans, ref PlayerLookMouseData data, ref Parent parent) =>
            {
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
                ComponentDataFromEntity<Translation> parentTrans = GetComponentDataFromEntity<Translation>(true);
                float3 pos = parentTrans[parent.Value].Value;

                float AngleRad = Mathf.Atan2(mouseWorldPosition.y - pos.y, mouseWorldPosition.x - pos.x);
                // Get Angle in Degrees
                float AngleDeg = (180 / Mathf.PI) * AngleRad;
                // Rotate Object
                rotation.Value = Quaternion.Euler(0, 0, AngleDeg);
            }).WithoutBurst().Run();
        
            return default;
        }
    }
}