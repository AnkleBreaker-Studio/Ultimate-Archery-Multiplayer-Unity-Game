using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public class PlayerControlsInputData : IComponentData
{
    public KeyCode MoveLeft;
    public KeyCode MoveRight;
    public KeyCode Jump;

    public KeyCode Shoot;
}
