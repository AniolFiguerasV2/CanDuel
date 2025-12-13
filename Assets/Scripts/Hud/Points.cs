using UnityEngine;

public class Points : MonoBehaviour
{
    public int points;
    public void AddPoints()
    {
        points++;
        HUDController.instance.UpdateHits(points);
    }
}
