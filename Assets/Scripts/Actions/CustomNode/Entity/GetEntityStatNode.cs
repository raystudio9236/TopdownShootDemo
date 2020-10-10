using Actions.Core;
using Components.Stat;
using XNode;

namespace Actions.CustomNode.Entity
{
    public class GetEntityStatNode : ActionNode
    {
        public StatFlag statFlag;
        [Output] public float Value;

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "Value")
                return entity.GetStat(statFlag);

            return null;
        }
    }
}