using UnityEngine;
using XNode;

namespace ActionGraph
{
    [CreateAssetMenu(fileName = "New Action Graph",
        menuName = "Action/ActionGraph")]
    public class ActionGraph : NodeGraph
    {
        public ActionGraphHost Host; // 宿主

        public GameEntity Entity => Host.Entity;
        
        public ActionNode EntryNode; // 入口节点
    }
}