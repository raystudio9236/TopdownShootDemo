using Actions.Core;

namespace Actions.CustomNode.Timer
{
    public class DelayFramesNode : ActionNode
    {
        [Input(ShowBackingValue.Never)] public byte In;
        [Output] public byte Finish;

        public int Frames;

        private long _frameTimerFlag;

        protected override void OnEnter()
        {
            _frameTimerFlag = host.Var.GetFlag(this, 0);
            host.Var.SetInt(_frameTimerFlag, 0);
        }

        protected override bool OnExecute()
        {
            var frames = host.Var.GetInt(_frameTimerFlag);
            frames++;
            
            if (frames > Frames)
            {
                host.Var.RemoveInt(_frameTimerFlag);
                
                ExitNode("Finish");
                return true;
            }

            host.Var.SetInt(_frameTimerFlag, frames);
            return false;
        }
    }
}