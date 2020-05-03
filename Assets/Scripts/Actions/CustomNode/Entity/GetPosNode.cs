using Actions.Core;
using UnityEngine;
using XNode;

namespace Actions.CustomNode.Entity
{
    public class GetPosNode : ActionNode
    {
        [Output] public Vector2 Pos;

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "Pos")
                return entity?.posComp.Value;

            return null;
        }
    }
}