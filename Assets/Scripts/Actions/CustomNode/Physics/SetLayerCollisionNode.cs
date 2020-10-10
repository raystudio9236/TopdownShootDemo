using Actions.CustomNode.Basic;
using Other;
using UnityEngine;

namespace Actions.CustomNode.Physics
{
    public class SetLayerCollisionNode : FlowActionNode
    {
        public PhysicsTag A;
        public PhysicsTag B;
        public bool Enable;

        protected override void OnFlowExecute()
        {
            Physics2D.IgnoreLayerCollision(
                A.ToValue(),
                B.ToValue(),
                !Enable);
        }
    }
}