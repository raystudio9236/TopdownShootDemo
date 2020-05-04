using Actions.CustomNode.Basic;
using Manager;

namespace Actions.CustomNode.Entity
{
    public class CreateFireCmdNode : FlowActionNode
    {
        [Input] public float Angle;
        [Input] public float Count;

        protected override void OnFlowExecute()
        {
            var angle = GetInputValue("Angle", Angle);
            var count = GetInputValue("Count", Count);

            var playerEntity = GameManager.Instance.Player;
            playerEntity.AddFireCmdComp(angle, (int) count);
        }
    }
}