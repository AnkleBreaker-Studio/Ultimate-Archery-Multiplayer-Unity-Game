using ECS.Scripts.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;

namespace ECS.Scripts.Authoring
{
    [GenerateAuthoringComponent]
    struct Level : IComponentData, ILevel
    {
        public int Difficulty { get; set; }

        public bool IsComplete { get; set; }

        public bool Failed { get; set; }

        public bool InProgress { get; set; }
    }
}
