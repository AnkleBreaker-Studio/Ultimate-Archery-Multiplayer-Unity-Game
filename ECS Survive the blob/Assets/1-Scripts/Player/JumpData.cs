using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct JumpData : IComponentData
{
    public bool isOnGround;
    public float jumpPower;
}

[GenerateAuthoringComponent]
public struct PlayerTag : IComponentData
{
}
