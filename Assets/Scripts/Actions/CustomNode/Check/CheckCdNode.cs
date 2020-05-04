using Actions.Core;
using Actions.CustomNode.Basic;
using Components.Base;

namespace Actions.CustomNode.Check
{
    public class CheckCdNode : FlowActionNode
    {
        public TimerFlag Flag;
        public bool ResetWhenFinish;

        [Output] public byte Finish;

        protected override void OnFlowExecute()
        {
            if (entity.CheckTimer(Flag))
            {
                if (ResetWhenFinish)
                    entity.ResetTimer(Flag);

                ExitNode("Finish");
            }
        }
    }
}