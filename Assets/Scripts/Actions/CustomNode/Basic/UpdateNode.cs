using Actions.Core;

namespace Actions.CustomNode.Basic
{
    public class UpdateNode : ActionNode
    {
        [Output] public byte Out;

        protected override bool OnExecute()
        {
            ExitNode("Out");

            return false;
        }
    }
}