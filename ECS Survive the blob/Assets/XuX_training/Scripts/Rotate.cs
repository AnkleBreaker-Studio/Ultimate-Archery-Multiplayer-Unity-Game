using Unity.Entities;
using UnityEngine;

namespace XuX_training.Scripts
{
    public struct Rotate : IComponentData
    {
        public float RadiansPerSecond;
    }
}
