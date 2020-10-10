using System;
using Events;
using Manager;
using UnityEngine;
using UnityEngine.UI;
using Utils.Pool;

namespace UI
{
    [Serializable]
    public class UIPopupItemPool : PrefabPool<UIPopupItem>
    {
    }

    public class UIPopupManager : MonoBehaviour
    {
        public UIPopupItemPool PopupItemPool;
        public CanvasScaler CanvasScaler;

        private Camera _uiCamera;

        private Vector2 _canvasResolution;

        private void Awake()
        {
            _uiCamera = Camera.main;
            _canvasResolution = CanvasScaler.referenceResolution;
        }

        private void Start()
        {
            GameManager.AddHandler<ChangeHpData>(GlobalEvent.ChangeHp,
                OnEntityChangeHp);
        }

        private void OnDestroy()
        {
            GameManager.RemoveHandler<ChangeHpData>(GlobalEvent.ChangeHp,
                OnEntityChangeHp);
        }

        private void OnEntityChangeHp(short eventtype,
            ChangeHpData data)
        {
            var item = PopupItemPool.Spawn();

            item.SetData(this,
                data.Target.posComp.Value,
                data.ChangeValue);
        }

        public Vector2 ConvertWorldPosToUIPos(Vector3 pos)
        {
            pos = _uiCamera.WorldToViewportPoint(pos);
            pos = new Vector2(
                _canvasResolution.x * (pos.x - 0.5f),
                _canvasResolution.y * (pos.y - 0.5f));
            return pos;
        }
    }
}