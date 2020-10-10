using System.Threading.Tasks;
using Other;
using TMPro;
using UI;
using UnityEngine;

public class UIPopupItem : MonoBehaviour
{
    public TMP_Text Text;
    public Color AddColor;
    public Color SubColor;

    private UIPopupManager _parent;
    private Vector3 _worldPos;

    public async void SetData(
        UIPopupManager parent,
        Vector3 worldPos,
        float value)
    {
        _parent = parent;
        _worldPos = worldPos;

        transform.RectTf().anchoredPosition =
            parent.ConvertWorldPosToUIPos(_worldPos);

        Text.text = value.ToString("0.0");
        Text.color = value > 0 ? AddColor : SubColor;

        gameObject.SetActive(true);

        await Task.Delay(500);

        if (gameObject == null)
            return;

        parent.PopupItemPool.Recycle(this);
    }

    private void LateUpdate()
    {
        transform.RectTf().anchoredPosition =
            _parent.ConvertWorldPosToUIPos(_worldPos);
    }
}