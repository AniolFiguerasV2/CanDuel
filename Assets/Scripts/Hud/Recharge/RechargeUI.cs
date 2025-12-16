using UnityEngine;
using UnityEngine.UI;

public class RechargeUI : MonoBehaviour
{
    public GameObject root;
    public RectTransform arrow;
    public Image bar;

    public float maxArrowX = 150f;

    void Start()
    {
        Show(false);
    }

    public void Show(bool state)
    {
        root.SetActive(state);
    }

    public void UpdateArrow(float normalizedPos)
    {
        float x = normalizedPos * maxArrowX;
        arrow.anchoredPosition = new Vector2(x, arrow.anchoredPosition.y);
    }

    public void SetBarSprite(Sprite s)
    {
        bar.sprite = s;
    }
}
