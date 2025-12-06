using UnityEngine;

public class Lifes : MonoBehaviour
{
    public int lifes = 3;

    private void Start()
    {
        HUDController controller = GetComponent<HUDController>();
        HUDController.instance.UpdateLifes(lifes);
    }

    public void LoseLife(int amount)
    {
        lifes -= amount;

        if(lifes < 0)
        {
            lifes = 0;
        }

        HUDController.instance.UpdateLifes(lifes);

        if( lifes <= 0)
        {
            Debug.Log("¡Sin vidas!");
        }
    }
}
