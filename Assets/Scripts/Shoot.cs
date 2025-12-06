using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    private PlayerInputs inputAction;
    public float raycastRange = 100f;

    public int maxBullets = 6;
    private int currentBullets;

    private bool isReloading = false;
    private float reloadTime = 0f;
    private float reloadDuration = 0f;

    public Points playerPointsScript;
    private bool abilityCharged = false;

    private bool infiniteAmmoActive = false;
    private float infiniteAmmoTimer = 0f;
    public float infiniteAmmoDuration = 15f;

    private void Awake()
    {
        inputAction = new PlayerInputs();
        inputAction.Enable();

        currentBullets = maxBullets;
    }
    private void Start()
    {
        inputAction.Player.shoot.performed += Shooting;
        inputAction.Player.recharge.performed += Recharge;
        inputAction.Player.infinitammo.performed += ActivateInfiniteAmmo;
        inputAction.Player.chargeHabilites.performed += ChargeAbility;
    }

    private void Update()
    {
        if (isReloading)
        {
            reloadTime -= Time.deltaTime;

            if (reloadTime <= 0f)
            {
                FinishReload();
            }
        }
        if (infiniteAmmoActive)
        {
            infiniteAmmoTimer -= Time.deltaTime;
            if (infiniteAmmoTimer <= 0f)
            {
                infiniteAmmoActive = false;
                Debug.Log("Munición infinita desactivada.");
            }
        }

        if (!abilityCharged && playerPointsScript.points >= 100)
        {
            ChargeAbility();
        }
    }

    private void Shooting(InputAction.CallbackContext obj)
    {
        
        if (isReloading){ return; }
        if (currentBullets <= 0) 
        {
            StartReload(1.5f);
            return;
        }
        if (!infiniteAmmoActive)
        {
            currentBullets--;
            HUDController.instance.UpdateAmmo(currentBullets);
        }

        Camera camera = Camera.main;
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = camera.ScreenPointToRay(mousePos);

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 0.1f);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, raycastRange)) 
        {
            if (hitInfo.collider.CompareTag("CAN"))
            {
                Can canscript = hitInfo.collider.GetComponent<Can>();

                if (canscript != null)
                {
                    canscript.OnHit(hitInfo.point);
                }
            }
        }
    }

    private void Recharge(InputAction.CallbackContext obj)
    {
        if (isReloading) { return; }
        if(currentBullets == maxBullets) { return; }

        StartReload(1f);
    }
    private void StartReload(float time)
    {
        isReloading = true;
        reloadDuration = time;
        reloadTime = time;
    }
    private void FinishReload()
    {
        isReloading = false;
        currentBullets = maxBullets;
        HUDController.instance.UpdateAmmo(currentBullets);
    }

    private void ChargeAbility(InputAction.CallbackContext obj)
    {
        ChargeAbility();
    }

    private void ChargeAbility()
    {
        abilityCharged = true;
        Debug.Log("¡Habilidad cargada!");
    }
    private void ActivateInfiniteAmmo(InputAction.CallbackContext obj)
    {
        infiniteAmmoActive = true;
        infiniteAmmoTimer = infiniteAmmoDuration;
        currentBullets = maxBullets;
        Debug.Log("Munición infinita activada por 15 segundos!");
    }
}