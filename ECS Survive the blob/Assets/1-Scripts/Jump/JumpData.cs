using Unity.Entities;

[GenerateAuthoringComponent]
public struct JumpData : IComponentData
{
    public bool IsOnGround;
    public float jumpPower;
}

[GenerateAuthoringComponent]
public struct PlayerTag : IComponentData
{
}
