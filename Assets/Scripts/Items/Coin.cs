using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private int _cost;

    public event UnityAction<int> Destroyed;

    private Rigidbody2D _rigidbody;
    private CircleCollider2D _collider;

    private Coroutine _coroutine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        MoveUp();

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(EnableCollision());
    }

    private void OnDisable()
    {
        Destroyed?.Invoke(_cost);
    }

    public void MoveUp()
    {
        float force = 8f;

        transform.SetParent(null);
        _rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        SoundPlayer.Instance.PlaySound(_clip);
    }

    private IEnumerator EnableCollision()
    {
        yield return new WaitForSeconds(0.5f);
        _collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Lava lava))
        {
            Destroy(gameObject);
        }
    }
}
