using System;
using System.Collections.Generic;

namespace ActionGraph
{
    /// <summary>
    /// 图状态
    /// </summary>
    [Serializable]
    public enum GraphState
    {
        Running,
        Done,
    }

    public class ActionGraphHost
    {
        public ActionGraph Graph { get; private set; }
        public GameEntity Entity { get; private set; }
        
        public GraphState State { get; private set; }

        private readonly List<ActionNode> _executeNodeList = 
            new List<ActionNode>();
        private readonly HashSet<ActionNode> _executeNodeSet =
            new HashSet<ActionNode>();

        public void SetData(ActionGraph graph, GameEntity entity)
        {
            Graph = graph;
            Entity = entity;

            if (graph.EntryNode != null)
            {
                _executeNodeList.Add(graph.EntryNode);
                _executeNodeSet.Add(graph.EntryNode);
            }

            State = GraphState.Running;
        }

        public void RegisterExecute(ActionNode node)
        {
            if (_executeNodeSet.Contains(node))
                return;
            
            _executeNodeList.Add(node);
            _executeNodeSet.Add(node);
        }

        public void Execute()
        {
            Graph.Host = this;

            switch (State)
            {
                case GraphState.Running:
                {
                    for (var i = _executeNodeList.Count - 1; i >= 0; i--)
                    {
                        var node = _executeNodeList[i];
                        if (node.HasExecuted || !node.Execute()) continue;

                        _executeNodeSet.Remove(node);
                        _executeNodeList.RemoveAt(i);
                    }

                    if (_executeNodeList.Count == 0)
                    {
                        State = GraphState.Done;
                    }
                    else
                    {
                        foreach (var actionNode in _executeNodeList)
                            actionNode.HasExecuted = false;
                    }

                    break;
                }

                case GraphState.Done:
                {
                    break;
                }
            }
            
            Graph.Host = null;
        }
    }
}