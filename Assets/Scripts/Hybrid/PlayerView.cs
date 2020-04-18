using Entitas.Unity;
using UnityEngine;

public class PlayerView : View, IPhysicsView
{
    [SerializeField] private Transform _shoot;
    [SerializeField] private Rigidbody2D _rigidbody;

    public Transform Shoot => _shoot;
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
