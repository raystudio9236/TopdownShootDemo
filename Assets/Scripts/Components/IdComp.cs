using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Input]
public sealed class IdComp : IComponent
{
    [PrimaryEntityIndex]
    public int Value;
}
