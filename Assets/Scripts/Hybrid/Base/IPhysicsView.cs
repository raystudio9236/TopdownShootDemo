using UnityEngine;

namespace Hybrid.Base
{
    public interface IPhysicsView : IView
    {
        Rigidbody2D Rigidbody { get; }
        Collider2D Collider { get; }
    }
}
