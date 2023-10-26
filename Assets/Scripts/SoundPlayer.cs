using UnityEngine;

public class SoundPlayer : MonoBehaviour
{    
    public static SoundPlayer Instance;

    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();

        Instance = null;

        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _audio.PlayOneShot(clip);
    }

    public void PlaySound(AudioClip[] clip)
    {
        int index = Random.Range(0, clip.Length-1);

        _audio.PlayOneShot(clip[index]);
    }
}
