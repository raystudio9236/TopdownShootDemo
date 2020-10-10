using Components.Physics;
using Components.Stat;
using Components.Target;
using Entitas;
using EZCameraShake;
using Hybrid.Base;
using Other;

namespace Systems.Base
{
    public class PhysicsSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;
        private readonly PhysicsComp _physics;

        public PhysicsSystem(Contexts contexts)
        {
            _contexts = contexts;
            _physics = contexts.physics.physicsComp;
        }

        public void Execute()
        {
            foreach (var collisionInfo in _physics.CollisionInfos)
            {
                var sourceEntity = _contexts.game.GetEntityWithIdComp(
                    collisionInfo.SourceId);
                var otherEntity = _contexts.game.GetEntityWithIdComp(
                    collisionInfo.OtherId);

                if (sourceEntity.isDestroyFlag
                    || otherEntity.isDestroyFlag)
                    continue;

                if (sourceEntity.isBulletTag)
                {
                    if (otherEntity.isEnemyTag)
                    {
                        sourceEntity.isDestroyFlag = true;
                        otherEntity.ChangeHp(
                            -sourceEntity.GetStat(StatFlag.Damage));

                        CameraShaker.Instance.ShakeOnce(1.5f, 1.5f, 0.1f, 0.3f);
                    }
                }
                else if (sourceEntity.isCoinTag)
                {
                    if (otherEntity.isPlayerTag)
                    {
                        if (!sourceEntity.hasTargetComp)
                        {
                            sourceEntity.viewComp.View.AsPhysics().Collider
                                .enabled = false;
                            
                            sourceEntity.AddTargetComp(
                                otherEntity.idComp.Value,
                                ActorTag.Player,
                                FindTargetType.Given,
                                LostTargetActionType.None);
                            
                            sourceEntity.RemoveLifetimeComp();
                        }
                    }
                }
                else if (sourceEntity.isEnemyTag)
                {
                    if (otherEntity.isPlayerTag)
                    {
                        CameraShaker.Instance.ShakeOnce(3f, 3f, 0.1f, 0.3f);

                        otherEntity.ChangeHp(
                            -1);
                    }
                }
            }

            _physics.CollisionInfos.Clear();
        }
    }
}