public class GameSystems : Feature
{
    public GameSystems(Contexts contexts)
    {
        // 玩家输入
        Add(new InputSystem(contexts));
        Add(new PlayerInputProcessSystem(contexts));

        // 移动
        Add(new MoveSystem(contexts));

        // 清理
        Add(new InputCleanupSystem(contexts));
    }
}