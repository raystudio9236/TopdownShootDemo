using Entitas.Unity;
using UnityEngine;

public class EnemyView : View, IPhysicsView
{
    [SerializeField] private Rigidbody2D _rigidbody;

    public Rigidbody2D Rigidbody => _rigidbody;

    protected override void OnDestroyEntityHandler()
    {
        base.OnDestroyEntityHandler();

        Destroy(gameObject);
    }
}