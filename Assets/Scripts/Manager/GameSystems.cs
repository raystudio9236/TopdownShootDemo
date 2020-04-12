public class GameSystems : Feature
{
    public GameSystems(Contexts contexts)
    {
        // 生成玩家
        Add(new PlayerSpawnSystem(contexts));
        
        // 玩家输入
        Add(new InputSystem(contexts));
        Add(new PlayerInputProcessSystem(contexts));
        
        // 移动
        Add(new MoveSystem(contexts));
        Add(new RotationSystem(contexts));
        
        Add(new FireSystem(contexts));

        Add(new AddViewSystem(contexts));
        
        Add(new LifetimeSystem(contexts));

        Add(new GameEventSystems(contexts));
        
        // 清理
        Add(new InputCleanupSystem(contexts));
        Add(new DestroySystem(contexts));
    }
}