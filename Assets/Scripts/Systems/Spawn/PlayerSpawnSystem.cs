using Entitas;
using Manager;
using UnityEngine;
using Utils;
using Utils.Entity;

namespace Systems.Spawn
{
    public class PlayerSpawnSystem : IInitializeSystem
    {
        private readonly Contexts _contexts;
    
        public PlayerSpawnSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Initialize()
        {
            var playerEntity = PlayerUtil.CreatePlayerEntity(_contexts, 
                Vector2.zero, 
                0);
            GameManager.Instance.Player = playerEntity;
        }
    }
}
