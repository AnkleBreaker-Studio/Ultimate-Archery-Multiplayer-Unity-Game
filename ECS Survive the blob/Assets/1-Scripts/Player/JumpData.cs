using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct JumpData : IComponentData
{
    public bool IsOnGround;
    public float JumpPower;
}

[GenerateAuthoringComponent]
public struct PlayerTag : IComponentData
{
}
