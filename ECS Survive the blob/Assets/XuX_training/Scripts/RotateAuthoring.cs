using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace XuX_training.Scripts
{
    public class RotateAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private float degreesPerSecond;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new Rotate { RadiansPerSecond = math.radians(degreesPerSecond) });
            dstManager.AddComponentData(entity, new RotationEulerXYZ());
        }
    }
}