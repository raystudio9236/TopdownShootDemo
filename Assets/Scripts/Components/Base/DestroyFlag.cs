using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Components.Base
{
    [Event(EventTarget.Self)]
    public sealed class DestroyFlag : IComponent
    {
    }
}