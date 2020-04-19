using Cinemachine;
using Entitas.Unity;
using UnityEngine;

public class PlayerView : View, IPhysicsView
{
    [SerializeField] private Transform _shoot;
    [SerializeField] private Rigidbody2D _rigidbody;

    public Transform Shoot => _shoot;
    public Rigidbody2D Rigidbody => _rigidbody;

    protected override void OnLinkEntityHandler()
    {
        base.OnLinkEntityHandler();

        var playerFollowCinemachine = FindObjectOfType<CinemachineVirtualCamera>();
        playerFollowCinemachine.Follow = transform;
    }

    protected override void OnDestroyEntityHandler()
    {
        base.OnDestroyEntityHandler();

        var playerFollowCinemachine = FindObjectOfType<CinemachineVirtualCamera>();
        playerFollowCinemachine.Follow = null;
        
        Destroy(gameObject);
    }
}
