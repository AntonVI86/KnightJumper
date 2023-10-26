using System.Collections;
using UnityEngine;

public class VerticalCameraMover : MonoBehaviour
{
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private KnightAbilities _abilities;
    [SerializeField] private Parallax _parallax;

    private float _speed;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _abilities.ScoreReached += OnChangeSpeed;
    }

    private void OnDisable()
    {
        _abilities.ScoreReached -= OnChangeSpeed;
    }

    private void Start()
    {
        _speed = _defaultSpeed;
        Time.timeScale = 1;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        float timeToLaunch = 3f;

        var delay = new WaitForSeconds(timeToLaunch);

        yield return delay;

        while (true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + _speed * Time.deltaTime, transform.position.z);
            yield return null;
        }
    }

    private void OnChangeSpeed()
    {
        _speed *= 1.3f;
    }
}
