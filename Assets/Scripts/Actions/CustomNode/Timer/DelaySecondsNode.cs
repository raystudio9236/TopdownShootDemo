using Actions.Core;
using UnityEngine;

namespace Actions.CustomNode.Timer
{
    public class DelaySecondsNode : ActionNode
    {
        [Input(ShowBackingValue.Never)] public byte In;
        [Output] public byte Finish;

        public float Seconds;

        private long _secondTimerFlag;

        protected override void OnEnter()
        {
            _secondTimerFlag = host.Var.GetFlag(this, 0);
            host.Var.SetFloat(_secondTimerFlag, 0);
        }

        protected override bool OnExecute()
        {
            var seconds = host.Var.GetFloat(_secondTimerFlag);
            seconds += Time.deltaTime;
            
            if (seconds > Seconds)
            {
                host.Var.RemoveInt(_secondTimerFlag);
                
                ExitNode("Finish");
                return true;
            }

            host.Var.SetFloat(_secondTimerFlag, seconds);
            return false;
        }
    }
}