using System.Collections.Generic;
using Actions.Core;
using Entitas;

public sealed class ActionComp : IComponent
{
    public List<ActionGraphHost> ActionGraphHostArr;
}