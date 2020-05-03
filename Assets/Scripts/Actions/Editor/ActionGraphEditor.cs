using System;
using Actions.Core;
using Actions.CustomNode;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace Actions.Editor
{
    [CustomNodeGraphEditor(typeof(ActionGraph))]
    public class ActionGraphEditor : NodeGraphEditor
    {
        private ActionGraph _actionGraph => target as ActionGraph;

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