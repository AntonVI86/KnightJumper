using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(KnightAbilities))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxGetter))]
public class KnightCollision : MonoBehaviour
{
    [SerializeField] private AudioClip _coin;
    [SerializeField] private AudioClip _damage;

    public event UnityAction DoorOpened;

    private bool _isDamaged = false;
    private AudioSource _audio;
    private KnightAbilities _abilities;
    private BoxGetter _boxGetter;

    private void Awake()
    {
        _abilities = GetComponent<KnightAbilities>();
        _audio = GetComponent<AudioSource>();
        _boxGetter = GetComponent<BoxGetter>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Coin coin))
        {
            _audio.PlayOneShot(_coin);
            coin.Destroyed += OnDestroyed;
            Destroy(coin.gameObject);
        } 
        
        if(collision.TryGetComponent(out Wizard wizard))
        {
            GetComponent<KnightMover>().ResetSpeed();
            GetComponent<KnightInput>().enabled = false;
            wizard.WritePharse();
        }

        if(collision.TryGetComponent(out HealthPotion healthPotion))
        {
            healthPotion.PlaySound();
            _abilities.AddHealth(1);
            collision.gameObject.SetActive(false);
        }

        if(collision.TryGetComponent(out Lava lava))
        {
            float maxDamage = 6;

            _abilities.TakeDamage(maxDamage);
        }

        if(collision.TryGetComponent(out Door door))
        {
            if(_abilities.PartsKey >= 5)
            {
                DoorOpened?.Invoke();
            }
        }

        if(collision.TryGetComponent(out Box box))
        {
            _abilities.AddBox();
            Destroy(box.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (_isDamaged == false)
            {
                _abilities.TakeDamage(enemy.Damage);
                _audio.PlayOneShot(_damage);
                _isDamaged = true;
                StartCoroutine(GetInvincibility());
            }
        }

        //if(collision.gameObject.TryGetComponent(out MagicSphere magicSphere))
        //{
        //    magicSphere.gameObject.SetActive(false);
        //    _abilities.ResetScore();
        //    SceneManager.LoadScene("Level1");
        //}

        if (collision.gameObject.TryGetComponent(out PlatformMover platform))
        {
            transform.SetParent(platform.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlatformMover platform))
        {
            transform.SetParent(null);
        }
    }

    private void OnDestroyed(int cost)
    {
        _abilities.AddMoney(cost);
    }

    private IEnumerator GetInvincibility()
    {
        yield return new WaitForSeconds(1f);
        _isDamaged = false;
    }

}