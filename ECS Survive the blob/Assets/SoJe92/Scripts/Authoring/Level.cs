﻿using ECS.Scripts.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace ECS.Scripts.Authoring
{
    [GenerateAuthoringComponent]
    public struct Level : IComponentData, ILevel
    {
        public int Difficulty;

        public bool IsComplete;

        public bool Failed;

        public bool InProgress;

        public int DeathCount;

        public void Start()
        {
            InProgress = true;
        }

        public void Fail()
        {
            Failed = true;
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
        }

        public void Reset()
        {
            Difficulty = 0;
        }
    }
}
