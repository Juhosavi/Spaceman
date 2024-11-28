using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVOlume(float level)
    {
       // audioMixer.SetFloat("MasterVolume", level);
       audioMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);

    }
    public void SetSoundFXVOlume(float level)
    {
        //audioMixer.SetFloat("SoundFXVolume", level);
        audioMixer.SetFloat("SoundFXVolume", Mathf.Log10(level) * 20f);
    }
    public void SetMusicOlume(float level)
    {
        //audioMixer.SetFloat("MusicVolume", level);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
    }

}
