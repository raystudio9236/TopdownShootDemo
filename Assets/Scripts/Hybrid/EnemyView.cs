using UnityEngine;

public class EnemyView : View, IPhysicsView
{
    [SerializeField] private Rigidbody2D _rigidbody;

    public Rigidbody2D Rigidbody => _rigidbody;
}
