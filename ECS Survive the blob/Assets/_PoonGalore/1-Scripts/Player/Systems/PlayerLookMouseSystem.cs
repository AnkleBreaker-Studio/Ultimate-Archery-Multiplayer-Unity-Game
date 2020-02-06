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
        private Vector3 _mouseWorldPosition;
        private ComponentDataFromEntity<Translation> _parentTrans;
        private float _angleRad;
        private float _angleDeg;
        private float3 _pos;
        private Camera _camera;
        
        protected override void OnCreate()
        {
            _camera = Camera.main;
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            Entities.ForEach((ref Rotation rotation, ref Translation trans, ref PlayerLookMouseData data, ref Parent parent) =>
            {
                if (_camera != null) _mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                _parentTrans = GetComponentDataFromEntity<Translation>(true);

                _pos = _parentTrans[parent.Value].Value;
                
                _angleRad = Mathf.Atan2(_mouseWorldPosition.y - _pos.y, _mouseWorldPosition.x - _pos.x);
                
                // Get Angle in Degrees
                _angleDeg = (180 / Mathf.PI) * _angleRad;
                
                // Rotate Object
                rotation.Value = Quaternion.Euler(0, 0, _angleDeg);
                
            }).WithoutBurst().Run();
        
            return default;
        }
    }
}