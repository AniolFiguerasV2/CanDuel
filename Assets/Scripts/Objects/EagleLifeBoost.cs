using UnityEngine;

public class EagleLifeBoost : MonoBehaviour
{
    private Lifes lifes;
    private bool used = false;

    private void Start()
    {
        lifes = FindAnyObjectByType<Lifes>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && !used)
        {
            used = true;

            if (lifes.lifes < 3)
            {
                lifes.AddLife(1);
            }
            Destroy(gameObject);
        }
    }
}
