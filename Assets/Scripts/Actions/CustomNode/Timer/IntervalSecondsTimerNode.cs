using Actions.Core;
using UnityEngine;

namespace Actions.CustomNode.Timer
{
    public class IntervalSecondsTimerNode : ActionNode
    {
        [Input(ShowBackingValue.Never)] public byte In;
        [Output] public byte Trigger;
        [Output] public byte Finish;
        
        public float IntervalSeconds;
        public int Times;

        private long _intervalSecondsTimerFlag;
        private long _timesFlag;

        protected override void OnEnter()
        {
            _intervalSecondsTimerFlag = host.Var.GetFlag(this, 0);
            _timesFlag = host.Var.GetFlag(this, 1);

            host.Var.SetFloat(_intervalSecondsTimerFlag, 0);
            host.Var.SetInt(_timesFlag, 0);
        }

        protected override bool OnExecute()
        {
            var timer = host.Var.GetFloat(_intervalSecondsTimerFlag);
            timer += Time.deltaTime;

            if (timer > IntervalSeconds)
            {
                var times = host.Var.GetInt(_timesFlag);
                times++;
                
                if (times > Times)
                {
                    host.Var.RemoveFloat(_intervalSecondsTimerFlag);
                    host.Var.RemoveInt(_timesFlag);
                    
                    ExitNode("Finish");
                    return true;
                }
                else
                {
                    host.Var.SetFloat(_intervalSecondsTimerFlag, 0);
                    host.Var.SetInt(_timesFlag, times);

                    ExitNode("Trigger");
                }
            }
            else
            {
                host.Var.SetFloat(_intervalSecondsTimerFlag, timer);
            }

            return false;
        }
    }
}