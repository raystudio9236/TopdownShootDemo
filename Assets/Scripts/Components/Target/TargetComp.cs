using Entitas;
using Other;

namespace Components.Target
{
    public sealed class TargetComp : IComponent
    {
        public int TargetId;
        public ActorTag TargetTag;
        public FindTargetType FindTargetType;
        public LostTargetActionType LostTargetActionType;
    }
}