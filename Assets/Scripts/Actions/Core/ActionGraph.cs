using System;
using UnityEngine;
using XNode;

namespace Actions.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NodeHideInCreateMenuAttribute : Attribute
    {
    }
    
    [CreateAssetMenu(fileName = "New Action Graph",
        menuName = "Action/ActionGraph")]
    public class ActionGraph : NodeGraph
    {
        public ActionGraphHost Host; // 宿主

        public GameEntity Entity => Host?.Entity;
        
        public ActionNode EntryNode; // 入口节点
        public ActionNode UpdateNode; // 每帧进行更新
    }
}