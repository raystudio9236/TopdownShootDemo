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
        public static short ChangeHp = AutoIndex;
    }
    
    public struct ChangeHpData
    {
        public GameEntity Target;
        public float ChangeValue;
    }
}