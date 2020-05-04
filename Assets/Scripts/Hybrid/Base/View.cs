using Entitas;
using Entitas.Unity;
using Manager;
using Other;
using UnityEngine;

namespace Hybrid.Base
{
    public class View : MonoBehaviour, IView, IDestroyFlagListener
    {
        [SerializeField] protected ActorTag actorTag;

        protected GameEntity _selfEntity =>
            gameObject.GetEntityLink().entity as GameEntity;

        public void Link(Contexts contexts, IEntity entity)
        {
            gameObject.Link(entity);

            var gameEntity = (GameEntity) entity;
            gameEntity.AddDestroyFlagListener(this);

            transform.position = gameEntity.posComp.Value;
            transform.rotation = Quaternion.Euler(0,
                0,
                gameEntity.rotComp.Angle);

            OnLinkEntityHandler();
        }

        public void OnDestroyFlag(GameEntity entity)
        {
            OnDestroyEntityHandler();
            gameObject.Unlink();
        }

        protected virtual void OnLinkEntityHandler()
        {
        }

        protected virtual void OnDestroyEntityHandler()
        {
            PoolManager.Instance.Recycle(this, actorTag);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var selfEntity = _selfEntity;
            var otherEntity =
                other.gameObject.GetEntityLink().entity as GameEntity;

            GameManager.Contexts.physics.physicsComp.CollisionInfos.Add(
                new CollisionInfo
                {
                    SourceId = selfEntity.idComp.Value,
                    OtherId = otherEntity.idComp.Value
                });
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var selfEntity = _selfEntity;
            var otherEntity =
                other.gameObject.GetEntityLink().entity as GameEntity;

            GameManager.Contexts.physics.physicsComp.CollisionInfos.Add(
                new CollisionInfo
                {
                    SourceId = selfEntity.idComp.Value,
                    OtherId = otherEntity.idComp.Value
                });
        }
    }
}