using Actions.CustomNode.Basic;
using UnityEngine;

namespace Actions.CustomNode.Util
{
    public class LogNode : FlowActionNode
    {
        [Input] public string Info;

        protected override void OnFlowExecute()
        {
            var info = GetInputValue("Info", Info);
            
            Debug.Log($"{Time.frameCount.ToString()} {info}");
        }
    }
}