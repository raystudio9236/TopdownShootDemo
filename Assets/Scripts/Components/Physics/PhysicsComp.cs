using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using Other;

namespace Components.Physics
{
    [Physics]
    [Unique]
    public sealed class PhysicsComp : IComponent
    {
        public List<CollisionInfo> CollisionInfos;
    }
}
