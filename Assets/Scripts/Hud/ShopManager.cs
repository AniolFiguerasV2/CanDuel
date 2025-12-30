using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public int phantomCanPrice = 60;
    public int shootGunPrice = 100;
    public int pistolPrice = 80;

    public Points playerPoints;

    public GameObject phantomCanPrefab;
    public GameObject shootGun;
    public GameObject pistol;
    public GameObject normalWeapon;

    private Vector3 spawnPosition = new Vector3(10.45f, 21.524f, -30.02f);

    private PlayerInputs inputAction;
    public GameObject shopCanvas;

    private GameObject currentTempWeapon;

    private void Awake()
    {
        inputAction = new PlayerInputs();
    }

    private void Start()
    {
        inputAction.Player.OpenMenu.performed += ToggleShop;
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Player.OpenMenu.performed -= ToggleShop;
        inputAction.Disable();
    }

    private void ToggleShop(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        bool isActive = !shopCanvas.activeSelf;
        shopCanvas.SetActive(isActive);
        Time.timeScale = isActive ? 0f : 1f;
    }

    public void BuyItemPhantomCan()
    {
        if (playerPoints.points >= phantomCanPrice)
        {
            playerPoints.points -= phantomCanPrice;
            HUDController.instance.UpdateHits(playerPoints.points);
            Instantiate(phantomCanPrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void BuyItemShootGun()
    {
        if (playerPoints.points >= shootGunPrice)
        {
            playerPoints.points -= shootGunPrice;
            HUDController.instance.UpdateHits(playerPoints.points);
            ActivateTemporaryWeapon(shootGun);
        }
    }

    public void BuyItemPistol()
    {
        if (playerPoints.points >= pistolPrice)
        {
            playerPoints.points -= pistolPrice;
            HUDController.instance.UpdateHits(playerPoints.points);
            ActivateTemporaryWeapon(pistol);
        }
    }

    private void ActivateTemporaryWeapon(GameObject weapon)
    {
        CancelInvoke(nameof(DeactivateTemporaryWeapon));

        if (currentTempWeapon != null)
            currentTempWeapon.SetActive(false);

        normalWeapon.SetActive(false);
        currentTempWeapon = weapon;
        currentTempWeapon.SetActive(true);

        Invoke(nameof(DeactivateTemporaryWeapon), 75f);
    }

    private void DeactivateTemporaryWeapon()
    {
        if (currentTempWeapon != null)
            currentTempWeapon.SetActive(false);

        normalWeapon.SetActive(true);
        currentTempWeapon = null;
    }
}
