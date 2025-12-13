using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    private PlayerInputs inputAction;

    public float raycastRange = 100f;

    public int maxBullets = 6;
    public int currentBullets;

    private void Awake()
    {
        inputAction = new PlayerInputs();
        inputAction.Enable();

        currentBullets = maxBullets;
    }

    private void Start()
    {
        inputAction.Player.shoot.performed += Shooting;
        HUDController.instance.UpdateAmmo(currentBullets);
    }

    private void Shooting(InputAction.CallbackContext obj)
    {
        if (currentBullets <= 0)
            return;

        currentBullets--;
        HUDController.instance.UpdateAmmo(currentBullets);

        Camera camera = Camera.main;
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = camera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, raycastRange))
        {
            if (hitInfo.collider.CompareTag("CAN"))
            {
                Can canscript = hitInfo.collider.GetComponent<Can>();
                if (canscript != null)
                    canscript.OnHit(hitInfo.point);
            }
        }
    }
    public void AddBullets(int amount)
    {
        currentBullets = Mathf.Clamp(currentBullets + amount, 0, maxBullets);
        HUDController.instance.UpdateAmmo(currentBullets);
    }

    public bool IsFull() => currentBullets >= maxBullets;
}