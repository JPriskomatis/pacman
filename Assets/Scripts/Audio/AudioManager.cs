using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource themesong, audioSource;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void StopThemeSong()
    {
        themesong.Stop();
    }

    public void PlayAudioInstance(AudioClip clip, float volume = 0.1f)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }

}
