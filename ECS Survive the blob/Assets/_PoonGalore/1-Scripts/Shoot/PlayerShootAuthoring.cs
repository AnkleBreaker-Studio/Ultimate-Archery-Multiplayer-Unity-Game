﻿using Unity.Physics;

namespace PoonGaloreECS    
{
    using System.Collections;
    using System.Collections.Generic;
    using Unity.Entities;
    using Unity.Jobs;
    using Unity.Mathematics;
    using Unity.Transforms;
    using UnityEngine;

    public struct GunData : IComponentData
    {
        public Entity Bullet;
        public float Strength;
        public float Rate;
        public float ShotDuration;

        public bool AutoFire;
        public bool IsFiring;
    }
    
    public class PlayerShootAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        public GameObject bullet;

        public float shootStrength;
        public float shootRate;
        public bool autoFire;
        
        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(bullet);
        }
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var a = dstManager.AddComponentData<GunData>(entity, new GunData
            {
                Bullet = conversionSystem.GetPrimaryEntity(bullet),
                Strength = shootStrength,
                Rate = shootRate,
                AutoFire = autoFire,
                IsFiring = false
            });
        }
    }
    
    public class PlayerShootingSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var fixedDeltaTime = Time.fixedDeltaTime;

            Entities.ForEach((ref LocalToWorld gunTransform, ref Rotation gunRotation, ref GunData gunData) =>
            {
                gunData.ShotDuration += fixedDeltaTime;

                if (!(gunData.Rate < gunData.ShotDuration) ||
                    (gunData.AutoFire ? !Input.GetKey(KeyCode.Mouse0) : !Input.GetKeyDown(KeyCode.Mouse0))) return;
                
                if (gunData.Bullet != null)
                {
                    var bullet = PostUpdateCommands.Instantiate(gunData.Bullet);
                        
                    var position = new Translation { Value = gunTransform.Position + gunTransform.Forward };
                    var rotation = new Rotation { Value = gunRotation.Value };
                    var velocity = new PhysicsVelocity
                    {
                        Linear = gunTransform.Forward * gunData.Strength,
                        Angular = float3.zero
                    };

                    PostUpdateCommands.SetComponent(bullet, position);
                    PostUpdateCommands.SetComponent(bullet, rotation);
                    PostUpdateCommands.SetComponent(bullet, velocity);

                    gunData.IsFiring = true;
                }

                gunData.ShotDuration = 0;
                gunData.IsFiring = false;
            });
        }
    }
}