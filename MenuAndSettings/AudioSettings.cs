using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] TextMeshProUGUI musicSliderText;
    public static float musicLevel = 1f;

    [SerializeField] Slider soundEffectsSlider;
    [SerializeField] TextMeshProUGUI soundEffectsSliderText;
    public static float soundEffectsLevel = 1f;

    [SerializeField] Slider uiSoundsSlider;
    [SerializeField] TextMeshProUGUI uiSoundsSliderText;
    public static float uiSoundsLevel = 1f;

    void Start()
    {
        musicSlider.onValueChanged.AddListener(delegate { MusicSliderCheck(); });

        soundEffectsSlider.onValueChanged.AddListener(delegate { SoundEffectsSliderCheck(); });

        uiSoundsSlider.onValueChanged.AddListener(delegate { UISoundsSliderCheck(); });
    }

    private void MusicSliderCheck()
    {
        musicSliderText.text = ((int)(musicSlider.value * 100)).ToString();
        musicLevel = musicSlider.value;
    }

    private void SoundEffectsSliderCheck()
    {
        soundEffectsSliderText.text = ((int)(soundEffectsSlider.value * 100)).ToString();
        soundEffectsLevel = soundEffectsSlider.value;
    }

    private void UISoundsSliderCheck()
    {
        uiSoundsSliderText.text = ((int)(uiSoundsSlider.value * 100)).ToString();
        uiSoundsLevel = uiSoundsSlider.value;
    }

    public void PortrayAudioSettings()
    {
        musicSliderText.text = ((int)(musicSlider.value * 100)).ToString();
        musicSlider.value = musicLevel;

        soundEffectsSliderText.text = ((int)(soundEffectsSlider.value * 100)).ToString();
        soundEffectsSlider.value = soundEffectsLevel;

        uiSoundsSliderText.text = ((int)(uiSoundsSlider.value * 100)).ToString();
        uiSoundsSlider.value = uiSoundsLevel;
    }

    public void ResetAudioSettings()
    {
        musicLevel = 1;
        soundEffectsLevel = 1;
        uiSoundsLevel = 1;

        PortrayAudioSettings();
    }

    public void ApplyAudioSettings()
    {
        masterMixer.SetFloat("Effects", Mathf.Log10(soundEffectsLevel) * 20);

        masterMixer.SetFloat("Music", Mathf.Log10(musicLevel) * 20);

        masterMixer.SetFloat("UI", Mathf.Log10(uiSoundsLevel) * 20);
    }
}
