using Actions.CustomNode.Basic;
using Manager;

namespace Actions.CustomNode.Entity
{
    public class CreateFireCmdNode : FlowActionNode
    {
        [Input] public float Angle;

        protected override void OnFlowExecute()
        {
            var angle = GetInputValue("Angle", Angle);
            var playerEntity = GameManager.Instance.Player;
            playerEntity.AddFireCmdComp(angle);
        }
    }
}