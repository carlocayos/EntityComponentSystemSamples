using Unity.Entities;
using Unity.Transforms;

namespace Samples.HelloNetcode
{
    [WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
    [UpdateInGroup(typeof(HelloNetcodeSystemGroup))]
    [UpdateAfter(typeof(BarrelSpawnerSystem))]
    public partial class StaticProblemSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<EnableOptimization>();
        }

        protected override void OnUpdate()
        {
            var setup = SystemAPI.GetSingleton<BarrelSetup>();
            if (!setup.EnableProblem)
            {
                Enabled = false;
            }

#if !ENABLE_TRANSFORM_V1
            /* This is intentionally incorrect. We grab write access to the transform data but never modify it. */
            Entities.ForEach((ref LocalTransform trans) =>
#else
            /* This is intentionally incorrect. We grab write access to the translation data but never modify it. */
            Entities.ForEach((ref Translation trans) =>
#endif
            {
            }).Run();
        }
    }
}
