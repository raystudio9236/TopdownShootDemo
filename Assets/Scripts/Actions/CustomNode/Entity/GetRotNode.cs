using Actions.Core;
using XNode;

namespace Actions.CustomNode.Entity
{
    public class GetRotNode : ActionNode
    {
        [Output] public float Angle;

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "Angle")
                return entity?.rotComp.Angle;
            
            return null;
        }
    }
}