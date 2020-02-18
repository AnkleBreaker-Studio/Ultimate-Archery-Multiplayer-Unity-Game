using Assets.SoJe92.Scripts.Authoring;
using ECS.Scripts.Authoring;
using ECS.Scripts.Contracts;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;

namespace ECS.Scripts.System_Data
{
    struct SpawnLocation : IComponentData
    {
        public int Limit;

        public bool IsRandom;

        public bool IsSpawning;

        //public List<Spawn> Spawned;

        public void End()
        {
            IsSpawning = false;
        }

        public bool IsReady()
        {
            // return Spawns.Any();
            return false;
        }

        public void Reset()
        {
            //Spawns.ForEach(x => x.Reset());
            //Spawned.Clear();
        }

        public List<Spawn> Start()
        {
            List<Spawn> spawned = new List<Spawn>();
            while (IsSpawning)
            {
                for (var i = 0; i > Limit; i++)
                {
                    if (IsRandom)
                    {
                        System.Random random = new System.Random();
                       // i = random.Next(Spawns.Count());
                    }
                   // var spawn = Spawns.ElementAt(i);
                    //Spawned.Add(spawn);
                }
            }
            End();
            //Spawned.AddRange(spawned);
            return spawned;
        }
    }
}
