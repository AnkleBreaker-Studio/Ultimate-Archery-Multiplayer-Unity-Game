using Assets.SoJe92.Scripts.Authoring;
using ECS.Scripts.Authoring;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace ECS.Scripts.Contracts
{
    public interface ISpawnLocation
    {
        List<Spawn> Start();

        void End();

        void Reset();

        bool IsReady();
    }
}
