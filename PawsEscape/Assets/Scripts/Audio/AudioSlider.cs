using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public enum VolumeType
    {
        Music,
        SFX
    }

    [SerializeField] private VolumeType volumeType;
    [SerializeField] private Slider slider;

    [Header("Music")]
    [SerializeField] private AudioSource musicSource;

    [Header("SFX")]
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        switch (volumeType)
        {
            case VolumeType.Music:
                float musicVolume = GameManager.Instance.MusicVolume;

                slider.value = musicVolume;
                musicSource.volume = musicVolume;
                slider.onValueChanged.AddListener(SetMusicVolume);
                break;

            case VolumeType.SFX:
                float sfxVolume = GameManager.Instance.SfxVolume;

                slider.value = sfxVolume;
                SetSfxVolume(sfxVolume);
                slider.onValueChanged.AddListener(SetSfxVolume);
                break;
        }
    }

    private void SetMusicVolume(float value)
    {
        musicSource.volume = value;
        GameManager.Instance.SetMusicVolume(value);
    }

    private void SetSfxVolume(float value)
    {
        GameManager.Instance.SetSfxVolume(value);
        audioMixer.SetFloat(
            "MasterVolume",
            Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20);
    }
}