using Entitas;
using UnityEngine;
using Utils;

namespace Systems.Spawn
{
    public class EnemySpawnSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;
        private float _timer = 0f;

        public EnemySpawnSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Execute()
        {
            var dt = Time.deltaTime;
            _timer += dt;

            if (_timer >= 1f)
            {
                _timer = 0f;

                var x = Random.Range(-9f, 9f);
                var y = Random.Range(-5f, 5f);
            
                var enemyEntity = EntityUtil.CreateEnemyEntity(_contexts, 
                    new Vector2(x, y), 
                    0f);

                var playerEntity = _contexts.game.playerTagEntity;
                enemyEntity.AddTargetComp(playerEntity.idComp.Value);
            }
        }
    }
}
