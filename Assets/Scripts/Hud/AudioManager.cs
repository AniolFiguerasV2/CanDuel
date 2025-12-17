using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class AudioManager : MonoBehaviour
{
    public float sfxVolume = 0.5f;
    public AudioMixerGroup sfxMixer;

    public float audioVolume = 0.5f;
    public AudioMixerGroup audioMixer;

    public GameObject menuCanvas;
    public PlayerInputs inputAction;

    private void Awake()
    {
        inputAction = new PlayerInputs();
    }
    private void Start()
    {
        inputAction.Player.escMenu.performed += OpenMenu;
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Player.escMenu.performed -= OpenMenu;
        inputAction.Disable();
    }

    public void SFXVolumeChange(float value)
    {
        sfxVolume = value * 2.5f;
        sfxMixer.audioMixer.SetFloat("VFXVolumen", Mathf.Log10(sfxVolume) * 20);
        if (sfxVolume <= 0f)
        {
            sfxMixer.audioMixer.SetFloat("VFXVolumen", -80f);
        }
    }

    public void AudioVolumeChange(float value)
    {
        audioVolume = value * 2.5f;
        audioMixer.audioMixer.SetFloat("SoundVolumen", Mathf.Log10(audioVolume) * 20);
        if (audioVolume <= 0f)
        {
            audioMixer.audioMixer.SetFloat("SoundVolumen", -80f);
        }
    }

    private void OpenMenu(InputAction.CallbackContext context)
    { 
        menuCanvas.SetActive(!menuCanvas.activeSelf);
    }
}
