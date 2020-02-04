using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public class ShootData : IComponentData
{
    public GameObject Bullet;
    public float ShootStrength;
}
