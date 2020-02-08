using Assets.SoJe92.Scripts.Authoring;
using ECS.Scripts.Authoring;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Scripts.Contracts
{
    interface ISpawnLocation
    {
        List<Spawn> Spawns { get; set; }

        List<Spawn> Spawned { get; set; }

        int Limit { get; set; }

        bool IsRandom { get; set; }

        bool IsSpawning { get; set; }

        List<Spawn> Start();

        void End();

        void Reset();

        bool IsReady();
    }
}
