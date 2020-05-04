using Cinemachine;
using Hybrid.Base;
using UnityEngine;

namespace Hybrid
{
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
}
