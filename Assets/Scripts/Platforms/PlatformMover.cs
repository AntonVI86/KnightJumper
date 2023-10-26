using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _minSpeed = 0.5f;
    private float _maxSpeed = 2f;

    private const float IdleState = 0;
    private const float MovingState = 1;

    private float currentState;

    private void Start()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
        currentState = IdleState;
    }
    private void Update()
    {
        switch (currentState)
        {
            case IdleState:
                _speed *= -1;
                currentState = MovingState;
                break;
            case MovingState:
                transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime, transform.position.y);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlatformBorder border))
        {
            currentState = IdleState;
        }
    }
}
