using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace PoonGaloreECS
{
    [GenerateAuthoringComponent]
    public struct TriggerEventData : IComponentData
    {
        public bool Enable;
    }

}
