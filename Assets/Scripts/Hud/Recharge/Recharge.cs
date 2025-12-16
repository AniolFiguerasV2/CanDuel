using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Recharge : MonoBehaviour
{
    private PlayerInputs inputAction;

    public Shoot shootScript;
    public RechargeUI rechargeUI;

    public RechargePattern[] patterns;
    private RechargePattern currentPattern;

    public float barMoveSpeed = 2f;
    public float cooldown = 3f;

    private bool isCharging = false;
    private bool isOnCooldown = false;
    private float barPosition = 0f;

    private float cooldownTimer = 0f;

    private void Awake()
    {
        inputAction = new PlayerInputs();
        inputAction.Enable();
    }

    private void Start()
    {
        inputAction.Player.recharge.performed += OnRechargePress;
    }

    private void Update()
    {
        if (isCharging)
        {
            barPosition = Mathf.Sin(Time.time * barMoveSpeed);
            rechargeUI.UpdateArrow(barPosition);
        }

        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                cooldownTimer = 0f;
                isOnCooldown = false;
            }
        }
    }

    private void OnRechargePress(InputAction.CallbackContext ctx)
    {
        if (isOnCooldown) return;

        if (!isCharging)
        {
            if (shootScript.IsFull()) return;
            StartRecharge();
        }
        else
        {
            StopRecharge();
        }
    }

    private void StartRecharge()
    {
        if (patterns == null || patterns.Length == 0)
        {
            Debug.LogWarning("No hay patrones de recarga configurados.");
            return;
        }

        currentPattern = patterns[Random.Range(0, patterns.Length)];

        barPosition = 0f;
        isCharging = true;

        rechargeUI.SetBarSprite(currentPattern.barSprite);
        rechargeUI.Show(true);
    }

    private void StopRecharge()
    {
        if (!isCharging) return;

        isCharging = false;
        rechargeUI.Show(false);

        int bullets = CalculateBullets(barPosition);
        shootScript.AddBullets(bullets);

        isOnCooldown = true;
        cooldownTimer = cooldown;
    }

    private int CalculateBullets(float pos)
    {
        if (currentPattern == null) return 1;

        float d = Mathf.Abs(pos);

        if (pos >= currentPattern.greenMin && pos <= currentPattern.greenMax)
            return 6;

        if (d < currentPattern.yellowRange) return 5;
        if (d < currentPattern.orangeRange) return 4;
        if (d < currentPattern.redRange) return 3;
        if (d < currentPattern.deepRedRange) return 2;

        return 1;
    }
}
