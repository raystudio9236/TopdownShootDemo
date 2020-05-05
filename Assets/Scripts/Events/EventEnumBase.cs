namespace Events
{
    public class EventEnumBase
    {
        protected static short _index = 0;

        protected static short AutoIndex
        {
            get { return _index++; }
        }
    }
}