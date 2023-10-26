using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float _health;
    [SerializeField] protected float _damage;
    [SerializeField] protected ParticleSystem _deathParticles;

    public event UnityAction Damaged;
    public event UnityAction Died;

    protected ParticleSystem _damageParticles;
    public float Damage => _damage;
    public float Health => _health;

    private void Awake()
    {
        _damageParticles = GetComponent<ParticleSystem>();   
    }

    public virtual void TakeDamage(float damage)
    {
        _health -= damage;
        _damageParticles.Play();
        Damaged?.Invoke();

        if(_health <= 0)
        {
            GameObject spawned = Instantiate(_deathParticles.gameObject, transform);
            spawned.transform.SetParent(null);
            Died?.Invoke();
            Destroy(transform.parent.gameObject);
        }
    }

    public void ResetHealth()
    {
        _health = 1;
    }
}
