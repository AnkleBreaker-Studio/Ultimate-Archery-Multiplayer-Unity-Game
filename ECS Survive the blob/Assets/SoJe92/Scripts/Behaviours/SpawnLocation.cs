using Assets.SoJe92.Scripts.Authoring;
using ECS.Scripts.Authoring;
using ECS.Scripts.Contracts;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;

namespace ECS.Scripts.Behaviours
{
    public class SpawnLocation : MonoBehaviour, /*ISpawnLocation,*/ IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        public List<GameObject> Spawns;

        public readonly int Limit;

        public readonly bool IsRandom;

        public bool IsSpawning;

        private List<GameObject> Spawned;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            foreach(var spawn in Spawns)
            {
                var spawnData = new Spawn
                {
                    Unit = conversionSystem.GetPrimaryEntity(spawn),
                    HasSpawned = true
                };
                var spawnEntity = dstManager.CreateEntity();
                dstManager.AddComponentData(spawnEntity, spawnData);
            }
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.AddRange(Spawns);
        }

        public void Update()
        {
            bool toSpawn = false;
        }

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
            Spawns.ForEach(x => x.GetComponent<Spawn>().Reset());
            Spawned.Clear();
        }

        //public void Start()
        //{
        //    List<GameObject> spawned = new List<GameObject>();
        //    while (IsSpawning)
        //    {
        //        for (var i = 0; i > Limit; i++)
        //        {
        //            if (IsRandom)
        //            {
        //                System.Random random = new System.Random();
        //                i = random.Next(Spawns.Count());
        //            }
        //            var spawn = Spawns.ElementAt(i);
        //            Spawned.Add(spawn);
        //        }
        //    }
        //    End();
        //    Spawned.AddRange(spawned);
        //    //return spawned.Select(x => x.GetComponent<Spawn>()).ToList();
        //}
    }
}
