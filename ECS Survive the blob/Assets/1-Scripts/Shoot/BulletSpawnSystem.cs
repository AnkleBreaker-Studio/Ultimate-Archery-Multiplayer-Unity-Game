using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class BulletSpawnSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps) 
    {
        

        return inputDeps;
    }

    private void SpawnBullet(GameObject bullet, Transform shootPoint, Vector3 rotation)
    {
        var bulletEntity = EntityManager.Instantiate(bullet);
        
        EntityManager.SetComponentData(bulletEntity, new Translation { Value = shootPoint.position });
        EntityManager.SetComponentData(bulletEntity, new Rotation { Value = Quaternion.Euler(rotation) });
        
        
    }
}
