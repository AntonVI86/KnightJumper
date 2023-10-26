using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class KnightAttacker : KnightAnimator
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private AudioClip[] _smashSound;
    [SerializeField] private AudioClip[] _kickMonster;

    private AudioSource _audio;

    private float _damage = 1;
    private float _radius = 0.3f;
    private float _timeToNewAttack = 1f;
    private bool _isCanAttack = true;

    private void Awake()
    {
        AnimatorPlayer = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    public void Attack(float horizontal)
    {
        float delay = 0.3f;

        if (_isCanAttack)
        {
            AttackAnimator(horizontal);
            int index = Random.Range(0, _smashSound.Length);
            _audio.PlayOneShot(_smashSound[index]);

            Invoke(nameof(KickEnemies), delay);

            _isCanAttack = false;

            StartCoroutine(AttackTimer());
        }
    }

    public void SetSide(float horizontal)
    {
        if (horizontal > 0)
        {
            _attackPoint.position = new Vector2(transform.position.x + 0.7119999f, _attackPoint.position.y);
        }
        if (horizontal < 0)
        {
            _attackPoint.position = new Vector2(transform.position.x - 0.7119999f, _attackPoint.position.y);
        }
    }

    private void KickEnemies()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(_attackPoint.position, _radius, _enemyLayer);
        
        foreach (var enemy in coll)
        {
            if(enemy.TryGetComponent(out Enemy monster))
            {
                int index = Random.Range(0, _kickMonster.Length);
                monster.TakeDamage(_damage);
                _audio.PlayOneShot(_kickMonster[index]);
            }

            if(enemy.TryGetComponent(out Chest chest))
                chest.OpenChest();           
        }
    }

    private IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(_timeToNewAttack);

        _isCanAttack = true;
    }
}
