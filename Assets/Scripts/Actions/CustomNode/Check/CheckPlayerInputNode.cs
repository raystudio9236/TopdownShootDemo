using Actions.Core;
using Manager;
using Other;

namespace Actions.CustomNode.Check
{
    public class CheckPlayerInputNode : ActionNode
    {
        [Input(ShowBackingValue.Never)] public byte In;
        [Output] public byte Trigger;

        public PlayerInputType InputType;
        public PlayerInput Input;

        protected override bool OnExecute()
        {
            var inputComp = GameManager.Contexts.input.inputComp;

            if (InputType == PlayerInputType.Keep)
            {
                if (Input == PlayerInput.MainButton
                    && inputComp.MainButton)
                {
                    ExitNode("Trigger");
                }
                else if (Input == PlayerInput.SecondaryButton
                         && inputComp.SecondaryButton)
                {
                    ExitNode("Trigger");
                }
            }
            else if (InputType == PlayerInputType.Down)
            {
                if (Input == PlayerInput.MainButton
                    && inputComp.MainButtonDown)
                {
                    ExitNode("Trigger");
                }
                else if (Input == PlayerInput.SecondaryButton
                         && inputComp.SecondaryButtonDown)
                {
                    ExitNode("Trigger");
                }
            }

            return true;
        }
    }
}