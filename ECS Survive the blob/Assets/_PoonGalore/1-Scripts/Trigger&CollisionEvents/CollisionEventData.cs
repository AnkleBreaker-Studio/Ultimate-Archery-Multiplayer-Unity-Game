using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace PoonGaloreECS
{
    [GenerateAuthoringComponent]
    public struct CollisionEventData : IComponentData
    {
        public bool Enable;
    }
}
