using Actions.Core;
using UnityEngine;

namespace Actions.CustomNode
{
    public class LogNode : ActionNode
    {
        [Input(ShowBackingValue.Never)] public byte In;
        [Output] public byte Out;
        
        [Input] public string Info;

        protected override bool OnExecute()
        {
            var info = GetInputValue("Info", Info);
            
            Debug.Log($"{Time.frameCount.ToString()} {info}");

            ExitNode("Out");
            
            return true;
        }
    }
}