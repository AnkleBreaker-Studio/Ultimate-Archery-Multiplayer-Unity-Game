using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class LookMouseSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Rotation rotation, ref Translation trans, ref LookMouseData data, ref Parent parent) =>
        {
            Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            ComponentDataFromEntity<Translation> parentTrans = GetComponentDataFromEntity<Translation>(true);
            float3 pos = parentTrans[parent.Value].Value;

            float AngleRad = Mathf.Atan2(mouseScreenPosition.y - pos.y, mouseScreenPosition.x - pos.x);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotate Object
            rotation.Value = Quaternion.Euler(0, 0, AngleDeg);
        });
    }
}
