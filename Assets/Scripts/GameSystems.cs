public class GameSystems : Feature
{
    public GameSystems(Contexts contexts)
    {
        // 玩家输入
        Add(new InputSystem(contexts));
        Add(new PlayerInputProcessSystem(contexts));
        
        // 移动
        Add(new MoveSystem(contexts));
        Add(new RotationSystem(contexts));

        Add(new AddViewSystem(contexts));
        
        // 清理
        Add(new InputCleanupSystem(contexts));
    }
}