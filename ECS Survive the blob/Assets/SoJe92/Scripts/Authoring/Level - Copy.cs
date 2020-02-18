using ECS.Scripts.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace ECS.Scripts.Authoring
{
    public class LevelBehaviour : MonoBehaviour, ILevel
    {
        public int Difficulty;

        public bool IsComplete;

        public bool Failed;

        public bool InProgress;

        public int DeathCount;

        public void Start()
        {
            IsComplete = false;
            InProgress = true;
        }

        public void Update()
        {
            if (IsComplete || Failed)
            {
                End();
            }
        }

        public void End()
        {
            InProgress = false;
            Failed = false;
            IsComplete = false;
            Difficulty = 0;
        }
    }
}
