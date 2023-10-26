using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    public const string EndScene = "EndScene";

    [SerializeField] private KnightCollision _knight;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _keysView;
    [SerializeField] private TMP_Text _keysText;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _knight.DoorOpened += OnDoorOpened;
    }

    private void OnDisable()
    {
        _knight.DoorOpened -= OnDoorOpened;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out KnightAbilities knight))
        {
            _keysView.SetBool("Show", true);
            _keysText.text = $"{knight.PartsKey}/5";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _keysView.SetBool("Show", false);
    }

    private void OnDoorOpened()
    {
        _animator.SetTrigger("Opened");
        _audioSource.Play();
        Invoke(nameof(LoadEndScene), 2f);
    }

    private void LoadEndScene()
    {
        SceneManager.LoadScene(EndScene);
    }
}
