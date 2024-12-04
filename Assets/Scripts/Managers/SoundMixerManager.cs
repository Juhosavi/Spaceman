using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider soundFXSlider;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        // Hae tallennetut arvot tai aseta oletusarvot
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        float soundFXVolume = PlayerPrefs.GetFloat("SoundFXVolume", 0.75f);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);

        // Päivitä AudioMixer
        UpdateAudioMixer("MasterVolume", masterVolume);
        UpdateAudioMixer("SoundFXVolume", soundFXVolume);
        UpdateAudioMixer("MusicVolume", musicVolume);

        // Päivitä sliderit
        masterSlider.value = masterVolume;
        soundFXSlider.value = soundFXVolume;
        musicSlider.value = musicVolume;

        // Varmista, että sliderit päivittävät arvot oikein
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        soundFXSlider.onValueChanged.AddListener(SetSoundFXVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    public void SetMasterVolume(float level)
    {
        UpdateAudioMixer("MasterVolume", level);
        PlayerPrefs.SetFloat("MasterVolume", level);
    }

    public void SetSoundFXVolume(float level)
    {
        UpdateAudioMixer("SoundFXVolume", level);
        PlayerPrefs.SetFloat("SoundFXVolume", level);
    }

    public void SetMusicVolume(float level)
    {
        UpdateAudioMixer("MusicVolume", level);
        PlayerPrefs.SetFloat("MusicVolume", level);
    }

    private void UpdateAudioMixer(string parameter, float level)
    {
        // Vältä nollan logaritmista käsittelyä
        float volume = Mathf.Log10(Mathf.Clamp(level, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(parameter, volume);
    }
}
