using Entitas.Unity;
using UnityEngine;

public class BulletView : View, IPhysicsView
{
    [SerializeField] private Rigidbody2D _rigidbody;

    public Rigidbody2D Rigidbody => _rigidbody;

    protected override void OnDestroyEntityHandler()
    {
        PoolManager.Instance.BulletPrefabPool.Recycle(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var selfEntity = _selfEntity;
        var otherEntity = other.gameObject.GetEntityLink().entity as GameEntity;

        GameManager.Contexts.physics.physicsComp.CollisionInfos.Add(
            new CollisionInfo
            {
                SourceId = selfEntity.idComp.Value,
                OtherId = otherEntity.idComp.Value
            });
    }
}