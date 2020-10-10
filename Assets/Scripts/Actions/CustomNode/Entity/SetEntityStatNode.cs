using Actions.CustomNode.Basic;
using Components.Stat;

namespace Actions.CustomNode.Entity
{
    public class SetEntityStatNode : FlowActionNode
    {
        public StatFlag statFlag;
        [Input] public float Value;

        protected override void OnFlowExecute()
        {
            var value = GetInputValue("Value", Value);
            entity.SetStat(statFlag, value);
        }
    }
}