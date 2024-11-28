using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;
    [SerializeField] private AudioSource soundFXobject;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    
    public void PlaySoundEffect(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //spawn in gameobjcet
        AudioSource audioSource = Instantiate(soundFXobject, spawnTransform.position, Quaternion.identity);

        //assing the audioclpi
        audioSource.clip = audioClip;

        //assing the volume
        audioSource.volume = volume;

        //playSound
        audioSource.Play();

        //get lengtht of sound FX clip
        float clipLenght = audioSource.clip.length;

        //destroy theclip after it is done playing
        Destroy(audioSource.gameObject, clipLenght);
    }
    public void PlayRandomSoundEffect(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        //rnadom number
        int rand = Random.Range(0, audioClip.Length);
        //spawn in gameobjcet
        AudioSource audioSource = Instantiate(soundFXobject, spawnTransform.position, Quaternion.identity);

        //assing the audioclpi
        audioSource.clip = audioClip[rand];

        //assing the volume
        audioSource.volume = volume;

        //playSound
        audioSource.Play();

        //get lengtht of sound FX clip
        float clipLenght = audioSource.clip.length;

        //destroy theclip after it is done playing
        Destroy(audioSource.gameObject, clipLenght);
    }
}
