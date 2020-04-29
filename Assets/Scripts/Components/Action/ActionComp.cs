using System.Collections.Generic;
using ActionGraph;
using Entitas;

public sealed class ActionComp : IComponent
{
    public List<ActionGraphHost> ActionGraphHostArr;
}