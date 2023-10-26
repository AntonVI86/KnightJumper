using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private AudioClip _sound;

    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponentInParent<AudioSource>();
    }

    public void PlaySound()
    {
        _audio.PlayOneShot(_sound);
    }
}
