using Actions.Core;

namespace Actions.CustomNode.Basic
{
    [NodeHideInCreateMenu]
    public class FlowActionNode : ActionNode
    {
        [Input(ShowBackingValue.Never)] public byte In;
        [Output] public byte Out;

        protected sealed override bool OnExecute()
        {
            OnFlowExecute();

            ExitNode("Out");

            return true;
        }

        protected virtual void OnFlowExecute()
        {
        }
    }
}