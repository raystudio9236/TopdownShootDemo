using Entitas;
using UnityEngine;

public class PlayerSpawnSystem : IInitializeSystem
{
    private readonly Contexts _contexts;
    
    public PlayerSpawnSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        EntityUtil.CreatePlayerEntity(_contexts, 
            Vector2.zero, 
            0);
    }
}
