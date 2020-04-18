using UnityEngine;

public interface IPhysicsView : IView
{
    Rigidbody2D Rigidbody { get; }
}
