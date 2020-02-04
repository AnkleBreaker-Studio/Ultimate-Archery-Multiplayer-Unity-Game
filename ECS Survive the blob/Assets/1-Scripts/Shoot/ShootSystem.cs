using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Transforms;
using UnityScript.Lang;

[AlwaysSynchronizeSystem]
public class ShootSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var deltaTime = Time.DeltaTime;

        Entities.ForEach((in PlayerControlsInputData inputData, in ShootData shootData) =>
        {
            if (Input.GetKeyDown(inputData.Shoot))
            {
                Debug.Log("Shoot");
            }
            
        }).WithoutBurst().Run();
        
        return default;
    }
}
