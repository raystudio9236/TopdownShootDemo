using Entitas.Unity;
using UnityEngine;

public class EnemyView : View, IPhysicsView
{
    [SerializeField] private Rigidbody2D _rigidbody;

    public Rigidbody2D Rigidbody => _rigidbody;

    private void OnCollisionEnter2D(Collision2D other)
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

    protected override void OnDestroyEntityHandler()
    {
        base.OnDestroyEntityHandler();

        Destroy(gameObject);
    }
}