using Actions.CustomNode.Basic;

namespace Actions.CustomNode.Entity
{
    public class SetEntityStatNode : FlowActionNode
    {
        public VarFlag VarFlag;
        [Input] public float Value;

        protected override void OnFlowExecute()
        {
            var value = GetInputValue("Value", Value);
            entity.SetStat(VarFlag, value);
        }
    }
}