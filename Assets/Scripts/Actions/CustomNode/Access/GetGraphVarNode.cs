using Actions.Core;
using XNode;

namespace Actions.CustomNode.Access
{
    [NodeHideInCreateMenu]
    public class GetGraphVarNode<T> : ActionNode
    {
        public string Key;
        [Output] public T Value;
        
        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "Value")
                return host.Var.GetGraphVar(Key);
            
            return null;
        }
    }
}