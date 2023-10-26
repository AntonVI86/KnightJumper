using UnityEngine;

public class DeathSoundPlaying : MonoBehaviour
{
    [SerializeField] private AudioClip[] _deathSounds;


    private void Start()
    {
        SoundPlayer.Instance.PlaySound(_deathSounds);
    }
}
