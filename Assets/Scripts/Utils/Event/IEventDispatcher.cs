namespace Utils.Event
{
    public interface IEventDispatcher
    {
        EventDispatcher EventDispatcher { get; }

        void AddHandler(short type, EventHandler handler);

        void AddHandler<T>(short type, EventHandler<T> handler);

        void RemoveHandler(short type, EventHandler handler);

        void RemoveHandler<T>(short type, EventHandler<T> handler);
    }
}