using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider VolumeSlider;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string mixerVolumeParameter = "MasterVolume";
    [SerializeField] private TextMeshProUGUI volumeLabel;

    private const float kMinDb = -80f;

    void Start()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 1f);
            volumeLabel.text = "100%";
        }
        Load();
        ApplyAudioSettings(VolumeSlider != null ? VolumeSlider.value : 1f);
    }

    public void ChangeVolume()
    {
        if (VolumeSlider == null) return;

        float linear = VolumeSlider.value;
        AudioListener.volume = linear;
        ApplyAudioSettings(linear);
        volumeLabel.text = Mathf.RoundToInt(linear * 100f) + "%";
        Save();
    }

    private void Load()
    {
        float saved = PlayerPrefs.GetFloat("volume");
        if (VolumeSlider != null)
            VolumeSlider.value = saved;
        AudioListener.volume = saved;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("volume", AudioListener.volume);
    }

    private void ApplyAudioSettings(float linear)
    {
        if (audioMixer == null || string.IsNullOrEmpty(mixerVolumeParameter)) return;

        float dB;
        if (linear <= 0f)
            dB = kMinDb;
        else
            dB = Mathf.Log10(Mathf.Clamp(linear, 0.0001f, 1f)) * 20f;

        audioMixer.SetFloat(mixerVolumeParameter, dB);
    }
}