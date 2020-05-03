using Actions.Core;

namespace Actions.CustomNode.Check
{
    public class CheckPlayerInputNode : ActionNode
    {
        [Input(ShowBackingValue.Never)] public byte In;
        [Output] public byte Trigger;

        public PlayerInput Input;

        protected override bool OnExecute()
        {
            var inputComp = GameManager.Contexts.input.inputComp;

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

            return true;
        }
    }
}