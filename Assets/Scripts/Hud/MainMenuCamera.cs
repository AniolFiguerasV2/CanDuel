using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public float baseY = -45f;
    public float amplitud = 0.5f;
    public float velocidad = 1f;

    private float tiempoInicial;

    void Start()
    {
        tiempoInicial = Time.time;
    }

    void Update()
    {
        float tiempo = Time.time - tiempoInicial;
        float anguloX = amplitud * Mathf.Sin(velocidad * tiempo);
        transform.localRotation = Quaternion.Euler(anguloX, baseY, 0f);
    }
}
