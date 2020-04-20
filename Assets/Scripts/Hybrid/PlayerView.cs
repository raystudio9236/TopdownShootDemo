using Cinemachine;
using Entitas.Unity;
using UnityEngine;

public class PlayerView : PhysicsView
{
    [SerializeField] private Transform _shoot;

    public Transform Shoot => _shoot;

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
    }
}
