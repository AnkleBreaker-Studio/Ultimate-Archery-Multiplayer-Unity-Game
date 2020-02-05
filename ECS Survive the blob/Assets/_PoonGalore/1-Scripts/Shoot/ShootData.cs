using Unity.Jobs;
using Unity.Entities;
using UnityEngine;

namespace PoonGaloreECS
{
    [GenerateAuthoringComponent]
    public class ShootData : IComponentData
    {
        public GameObject Bullet;
        public Transform ShootPoint;
        public float ShootStrength;
    }
}
