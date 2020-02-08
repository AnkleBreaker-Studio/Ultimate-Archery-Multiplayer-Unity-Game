using Assets.SoJe92.Scripts.Authoring;
using ECS.Scripts.Contracts;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;

namespace ECS.Scripts.Authoring
{
    [GenerateAuthoringComponent]
    struct SpawnLocation : IComponentData, ISpawnLocation
    {
        public List<Spawn> Spawns { get; set; }

        public int Limit { get; set; }

        public bool IsRandom { get; set; }

        public bool IsSpawning { get; set; }
        public List<GameObject> Spawned { get; set; }

        public void End()
        {
            IsSpawning = false;
        }

        public bool IsReady()
        {
            return Spawns.Any();
        }

        public void Reset()
        {
            Spawns.ForEach(x => x.Reset());
            Spawned.Clear();
        }

        public List<GameObject> Start()
        {
            List<GameObject> spawned = new List<GameObject>();
            while (IsSpawning)
            {
                for (var i = 0; i > Limit; i++)
                {
                    if (IsRandom)
                    {
                        System.Random random = new System.Random();
                        i = random.Next(Spawns.Count);
                    }
                    var spawn = Spawns[i];
                    //spawned.Add(spawn.Execute());
                    //Spawned.Add(spawn);
                    Spawns.Remove(spawn);
                }
            }
            End();
            return spawned;

        }
    }
}
