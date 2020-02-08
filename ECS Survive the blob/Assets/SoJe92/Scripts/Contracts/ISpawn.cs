using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;

namespace ECS.Scripts.Contracts
{
    public interface ISpawn
    {
        Entity Unit { get; set; }

        bool HasSpawned { get; set; }

        public void Reset();
    }
}
