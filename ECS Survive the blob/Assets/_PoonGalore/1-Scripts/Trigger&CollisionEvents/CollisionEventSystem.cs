using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

namespace PoonGaloreECS
{
    // EndFramePhysicsSystem
    // A system which combines the dependencies of all other physics jobs created during this frame into a single handle,
    // so that any system which depends on all physics work to be finished can just depend on this single handle.

    // Updating before or after a system constrains the scheduler ordering of these systems within a ComponentSystemGroup.
    // Both the before & after system must be a members of the same ComponentSystemGroup.
    // [UpdateBefore(typeof(EndFramePhysicsSystem))]
    // [UpdateAfter(typeof(EndFramePhysicsSystem))]
    
    // Updating in a group means this system will be automatically updated by the specified ComponentSystemGroup.
    // The system may order itself relative to other systems in the group with UpdateBegin and UpdateEnd,
    // There is nothing preventing systems from being in multiple groups, it can be added if there is a use-case for it
    // [UpdateInGroup(typeof(EndFramePhysicsSystem))]

    [UpdateBefore(typeof(StepPhysicsWorld))]
    public class CollisionEventSystem : JobComponentSystem
    {
        private BuildPhysicsWorld _buildPhysicsWorldSystem;
        private StepPhysicsWorld _stepPhysicsWorldSystem;

        protected override void OnCreate() //getting world physics systems for schedule
        {
            _buildPhysicsWorldSystem = World.GetOrCreateSystem<BuildPhysicsWorld>();
            _stepPhysicsWorldSystem = World.GetExistingSystem<StepPhysicsWorld>();
        }
        
        [BurstCompile]
        private struct CollisionEventJob : ICollisionEventsJob
        {
            [ReadOnly] public ComponentDataFromEntity<CollisionEventData> CollisionData;
            public ComponentDataFromEntity<PhysicsVelocity> PhysicsVelocityGroup; //must have inside otherwise system wont read the job correctly even if unused

            private CollisionEventData _componentA;
            private CollisionEventData _componentB;
            public void Execute(CollisionEvent collisionEvent)
            {
                var entityA = collisionEvent.Entities.EntityA;
                var entityB = collisionEvent.Entities.EntityB;
                
                if (!CollisionData.HasComponent(entityA)) return;
            
                var entityAExists = CollisionData.Exists(entityA);
                var entityBExists = CollisionData.Exists(entityB);
            
                _componentA = CollisionData[entityA]; 
                if (entityBExists) _componentB = CollisionData[entityB]; 
                
                if (_componentA.Enable && entityAExists) 
                {
                    Debug.Log("Collided with entity index " + entityB.Index);
                }
            
                if (entityBExists && _componentB.Enable) 
                {
                    //Debug.Log("Collided with entity index " + entityB.Index);
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var jobHandle = new CollisionEventJob
                {
                    CollisionData = GetComponentDataFromEntity<CollisionEventData>(true), //get the component data from both collided entities 
                    PhysicsVelocityGroup = GetComponentDataFromEntity<PhysicsVelocity>()
                }
                .Schedule(_stepPhysicsWorldSystem.Simulation, ref _buildPhysicsWorldSystem.PhysicsWorld, inputDeps);
            
            return jobHandle;
        }
    }
}