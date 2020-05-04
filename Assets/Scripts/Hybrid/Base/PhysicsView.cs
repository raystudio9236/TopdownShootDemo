using UnityEngine;

namespace Hybrid.Base
{
    public class PhysicsView : View, IPhysicsView
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Collider2D _collider;

        public Rigidbody2D Rigidbody => _rigidbody;
        public Collider2D Collider => _collider;
    }
}