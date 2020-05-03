using Actions.Core;
using Actions.CustomNode.Basic;

namespace Actions.CustomNode.Access
{
    [NodeHideInCreateMenu]
    public class SetGraphVarNode<T> : FlowActionNode
    {
        public string Key;
        [Input] public T Value;

        protected override void OnFlowExecute()
        {
            var value = GetInputValue("Value", Value);
            host.Var.SetGraphVar(Key, value);
        }
    }
}