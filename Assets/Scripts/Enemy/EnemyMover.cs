using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _minSpeed = 1f;
    [SerializeField] private float _maxSpeed = 2f;
    [SerializeField] private float _colliderOffset;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    private BoxCollider2D _collider;

    [SerializeField]private float _speed;
    private float _angle;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
        SetSide();
    }

    private void Update()
    {
        _rigidbody.velocity = Vector2.right * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EnemyBorder border))
        {
            _speed *= -1;
            SetSide();           
        }
    }

    public void SetSide()
    {
        if (_speed > 0)
        {
            _renderer.flipX = true;
            _collider.offset = new Vector2(-0.1f, -0.02f);
        }
        else if (_speed < 0)
        {
            _renderer.flipX = false;
            _collider.offset = new Vector2(0.08f, -0.02f);
        }
    }

    public void SetSpeed()
    {
        _speed *= -1;
    }
}


