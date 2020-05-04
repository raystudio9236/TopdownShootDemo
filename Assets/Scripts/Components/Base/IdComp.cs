using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Components.Base
{
    [Game, Input]
    public sealed class IdComp : IComponent
    {
        [PrimaryEntityIndex]
        public int Value;
    }
}
