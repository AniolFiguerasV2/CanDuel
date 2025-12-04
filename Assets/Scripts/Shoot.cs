using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    private PlayerInputs inputAction;
    public float raycastRange = 100f;

    private void Awake()
    {
        inputAction = new PlayerInputs();
        inputAction.Enable();
    }
    private void Start()
    {
        inputAction.Player.shoot.performed += Shooting;
    }

    private void Shooting(InputAction.CallbackContext obj)
    {
        Camera camera = Camera.main;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        Ray ray = camera.ScreenPointToRay(mousePos);

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 0.1f);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, raycastRange)) 
        {
            if (hitInfo.collider.CompareTag("CAN"))
            {
                Debug.Log("La lata ha sido impactada por el disparo!");
                Can canscript = hitInfo.collider.GetComponent<Can>();

                if (canscript != null)
                {
                    canscript.OnHit(hitInfo.point);
                }
            }
        }
    }
}
