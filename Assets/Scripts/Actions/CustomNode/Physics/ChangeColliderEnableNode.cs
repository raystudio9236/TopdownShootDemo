using Actions.CustomNode.Basic;
using Hybrid.Base;

namespace Actions.CustomNode.Physics
{
    public class ChangeColliderEnableNode : FlowActionNode
    {
        public bool Enable;

        protected override void OnFlowExecute()
        {
            var collider = entity.viewComp.View.AsPhysics().Collider;
            collider.enabled = Enable;
        }
    }
}