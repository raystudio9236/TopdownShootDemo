using Entitas;
using Other;

namespace Components.Target
{
    public enum FindTargetType
    {
        Given, // 给定目标
        Closet, // 最近目标
    }

    public enum LostTargetActionType
    {
        Keep, // 继续寻找
        None, // 无行为
    }
    
    public sealed class FindTargetCmdComp : IComponent
    {
        public ActorTag TargetTag;
        public FindTargetType FindTargetType;
        public LostTargetActionType LostTargetActionType;
    }
}