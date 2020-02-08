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

    [UpdateAfter(typeof(EndFramePhysicsSystem))]
    public class TriggerEventSystem : JobComponentSystem
{
    private BuildPhysicsWorld _buildPhysicsWorldSystem;
    private StepPhysicsWorld _stepPhysicsWorldSystem;

    protected override void OnCreate()
    {
        _buildPhysicsWorldSystem = World.GetOrCreateSystem<BuildPhysicsWorld>();
        _stepPhysicsWorldSystem = World.GetOrCreateSystem<StepPhysicsWorld>();
    }

    [BurstCompile]
    private struct TriggerEventJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentDataFromEntity<TriggerEventData> EntityData;
        
        private TriggerEventData _componentA;
        private TriggerEventData _componentB;
        
        public void Execute(TriggerEvent triggerEvent)
        {
            var entityA = triggerEvent.Entities.EntityA;
            var entityB = triggerEvent.Entities.EntityB;

            var entityAExists = EntityData.Exists(entityA);
            var entityBExists = EntityData.Exists(entityB);
            
            _componentA = EntityData[entityA]; //collider that is the one initiating the trigger
            if (entityBExists) _componentB = EntityData[entityB]; //collider that is being triggered 

            //could have logic for both triggered entities in a single trigger job, or
            //split them up based on the needs of the system
            
            /*
            if (_componentA.Enable) //logic for triggering entity
            {
                Debug.Log("Trigger A Enabled");
            }
            
            if (_componentB.Enable) //logic for triggered entity
            {
                Debug.Log("Trigger B Enabled");
            }
        */
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var jobHandle = new TriggerEventJob //get any required data from the entity
        {
            EntityData = GetComponentDataFromEntity<TriggerEventData>(true) //get the component data from both the triggered entities 

        }.Schedule(_stepPhysicsWorldSystem.Simulation, ref _buildPhysicsWorldSystem.PhysicsWorld, inputDeps);

        return jobHandle;
    }            
}
}
