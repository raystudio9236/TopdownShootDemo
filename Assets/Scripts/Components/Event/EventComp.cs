using Entitas;
using Utils.Event;

namespace Components.Event
{
    public sealed class EventComp : IComponent
    {
        public EventDispatcher Dispatcher;
    }
}