using System.Collections.Generic;
using Actions.Core;
using Entitas;

namespace Components.Action
{
    public sealed class ActionComp : IComponent
    {
        public List<ActionGraphHost> ActionGraphHostArr;
    }
}