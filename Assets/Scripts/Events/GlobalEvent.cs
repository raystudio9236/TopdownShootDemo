namespace Events
{
    public static class GlobalEvent
    {
        private static short _index = 0;

        private static short AutoIndex
        {
            get { return _index++; }
        }

        public static short PlayerSpawn = AutoIndex;
    }
}