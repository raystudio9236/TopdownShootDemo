using Actions.CustomNode.Basic;
using Hybrid.Base;
using Other;

namespace Actions.CustomNode.Physics
{
    public class MovePosNode : FlowActionNode
    {
        [Input] public float Angle;
        public float Dis;
        
        protected override void OnFlowExecute()
        {
            var angle = GetInputValue("Angle", Angle);
            
            var rigidbody2d = entity.viewComp.View.AsPhysics().Rigidbody;
            var pos = angle.Angle2Vector2D() * Dis + entity.posComp.Value;
            rigidbody2d.MovePosition(pos);
        }
    }
}