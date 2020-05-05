namespace Events
{
    public class GlobalEvent : EventEnumBase
    {
        public static readonly short PlayerSpawn = AutoIndex;
        public static readonly short ChangeHp = AutoIndex;
        public static readonly short SpawnBullet = AutoIndex;
    }

    public struct ChangeHpData
    {
        public GameEntity Target;
        public float ChangeValue;
    }

    public struct SpawnBulletData
    {
        public GameEntity Host;
        public GameEntity Bullet;
    }
}