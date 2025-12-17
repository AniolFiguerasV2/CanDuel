using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    private PlayerInputs inputAction;

    public float raycastRange = 400f;
    public int maxBullets = 6;
    public int currentBullets;

    public Transform muzzlePoint;
    public ParticleSystem muzzleFlash;
    public ParticleSystem hitEffectCan;
    public ParticleSystem hitEffectEagle;

    public AudioSource shootAudioSource;
    public AudioClip shootSound;

    public float recoilUp = 2.5f;
    public float recoilBack = 0.05f;
    public float recoilReturnSpeed = 20f;

    public GameObject bulletTracerPrefab;
    public float tracerDuration = 0.1f;

    private Vector3 originalLocalPosition;
    private Quaternion originalLocalRotation;

    private void Awake()
    {
        inputAction = new PlayerInputs();

        currentBullets = maxBullets;

        originalLocalPosition = transform.localPosition;
        originalLocalRotation = transform.localRotation;
    }

    private void Start()
    {
        inputAction.Enable();
        inputAction.Player.shoot.performed += Shooting;
    }

    private void OnDisable()
    {
        inputAction.Player.shoot.performed -= Shooting;
        inputAction.Disable();
    }

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            originalLocalPosition,
            Time.deltaTime * recoilReturnSpeed
        );

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            originalLocalRotation,
            Time.deltaTime * recoilReturnSpeed
        );
    }

    private void Shooting(InputAction.CallbackContext obj)
    {
        if (!this || currentBullets <= 0) return;

        currentBullets--;

        if (HUDController.instance != null)
            HUDController.instance.UpdateAmmo(currentBullets);

        if (shootAudioSource != null && shootSound != null)
        {
            shootAudioSource.PlayOneShot(shootSound);
        }

        if (muzzleFlash != null && muzzlePoint != null)
        {
            ParticleSystem flash = Instantiate(
                muzzleFlash,
                muzzlePoint.position,
                muzzlePoint.rotation
            );
            flash.Play();
            Destroy(flash.gameObject, 1f);
        }

        ApplyRecoil();

        Camera camera = Camera.main;
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = camera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, raycastRange))
        {
            if (bulletTracerPrefab != null)
            {
                GameObject tracer = Instantiate(bulletTracerPrefab, muzzlePoint.position, Quaternion.identity);
                LineRenderer lr = tracer.GetComponent<LineRenderer>();
                if (lr != null)
                {
                    lr.SetPosition(0, muzzlePoint.position);
                    lr.SetPosition(1, hitInfo.point);
                }
                Destroy(tracer, tracerDuration);
            }

            if (hitInfo.collider.CompareTag("CAN"))
            {
                if (hitEffectCan != null)
                {
                    ParticleSystem impact = Instantiate(
                        hitEffectCan,
                        hitInfo.point,
                        Quaternion.LookRotation(hitInfo.normal)
                    );
                    impact.Play();
                    Destroy(impact.gameObject, 2f);
                }

                Can canScript = hitInfo.collider.GetComponent<Can>();
                if (canScript != null)
                    canScript.OnHit(hitInfo.point);
            }
            else if (hitInfo.collider.CompareTag("EAGLE"))
            {
                if (hitEffectEagle != null)
                {
                    ParticleSystem impact = Instantiate(
                        hitEffectEagle,
                        hitInfo.point,
                        Quaternion.LookRotation(hitInfo.normal)
                    );
                    impact.Play();
                    Destroy(impact.gameObject, 2f);
                }

                Eagle eagleScript = hitInfo.collider.GetComponent<Eagle>();
                if (eagleScript != null)
                    eagleScript.OnHit(hitInfo.point);
            }
            else if (hitInfo.collider.CompareTag("PHANTOMCAN"))
            {
                if (hitEffectCan != null)
                {
                    ParticleSystem impact = Instantiate(
                        hitEffectCan,
                        hitInfo.point,
                        Quaternion.LookRotation(hitInfo.normal)
                    );
                    impact.Play();
                    Destroy(impact.gameObject, 2f);
                }

                PhantomCan phantomCanScript = hitInfo.collider.GetComponent<PhantomCan>();
                if (phantomCanScript != null)
                    phantomCanScript.OnHit(hitInfo.point);
            }
        }
    }

    private void ApplyRecoil()
    {
        transform.localRotation *= Quaternion.Euler(-recoilUp, 0f, 0f);
        transform.localPosition += Vector3.back * recoilBack;
    }

    public void AddBullets(int amount)
    {
        currentBullets = Mathf.Clamp(currentBullets + amount, 0, maxBullets);
        if (HUDController.instance != null)
            HUDController.instance.UpdateAmmo(currentBullets);
    }

    public bool IsFull() => currentBullets >= maxBullets;
}