using System;
using System.Linq;
using Actions.Core;
using Actions.CustomNode;
using Actions.CustomNode.Basic;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace Actions.Editor
{
    [CustomNodeGraphEditor(typeof(ActionGraph))]
    public class ActionGraphEditor : NodeGraphEditor
    {
        private ActionGraph _actionGraph => target as ActionGraph;
        
        public override string GetNodeMenuName(Type type)
        {
            if (type.CustomAttributes != null
                && type.CustomAttributes.Select(data => data.AttributeType == typeof(NodeHideInCreateMenuAttribute))
                    .Count() != 0)
                return null;
        
            if (type.FullName != null && type.FullName.Contains("Actions.Core"))
                return null;
        
            if (type.IsSubclassOf(typeof(ActionNode)) || type.IsAssignableFrom(typeof(ActionNode)))
            {
                return "ActionNode/" + base.GetNodeMenuName(type).Replace("Actions/Custom Node/", "");
            }
        
            return null;
        }

        public override Node CreateNode(Type type, Vector2 position)
        {
            var node = base.CreateNode(type, position);

            if (type == typeof(EntryNode))
            {
                _actionGraph.EntryNode = node as ActionNode;
            }
            else if (type == typeof(UpdateNode))
            {
                _actionGraph.UpdateNode = node as ActionNode;
            }

            return node;
        }
    }
}