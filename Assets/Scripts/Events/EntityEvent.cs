namespace Events
{
    public class EntityEvent : EventEnumBase
    {
        public static readonly short SpawnBullet = AutoIndex;
    }
    
    public struct EntitySpawnBulletData
    {
        public GameEntity Host;
        public GameEntity Bullet;
    }
}