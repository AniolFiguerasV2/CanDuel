
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;

    public TextMeshProUGUI hitsText;
    public TextMeshProUGUI ammoText;

    public Image[] hearts;
    public Sprite nomralHeart;
    public Sprite brokenHeart;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateAmmo(int value)
    {
        ammoText.text = value + " / \u221E";
    }
    public void UpdateHits(int value)
    {
            hitsText.text = value.ToString();
    }
    public void UpdateLifes(int lifes)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < lifes)
            {
                hearts[i].sprite = nomralHeart;
            }
            else
            {
                hearts[i].sprite = brokenHeart;
            }
        }
    }
}
