using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BatMover : MonoBehaviour
{
    [SerializeField] private float _minSpeed = 1f;
    [SerializeField] private float _maxSpeed = 2f;
    [SerializeField] private float _colliderOffset;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;

    private float _speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }

    private void Update()
    {
        _rigidbody.velocity = Vector2.left * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out KnightAbilities knight))
        {
            _collider.enabled = false;
        }
    }
}