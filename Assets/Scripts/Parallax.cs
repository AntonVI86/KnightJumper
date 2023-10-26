using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class Parallax : MonoBehaviour
{
    [SerializeField] private float _defaultSpeedY;
    [SerializeField] private KnightAbilities _abilities;

    private float _speedY;

    private Coroutine _coroutine;
    private RawImage _image;
    private float _imagePositionY;

    private void Awake()
    {
        _image = GetComponent<RawImage>();
    }

    private void OnEnable()
    {
        _abilities.ScoreReached += ChangeSpeed;
    }

    private void OnDisable()
    {
        _abilities.ScoreReached -= ChangeSpeed;
    }

    private void Start()
    {
        _speedY = _defaultSpeedY;

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
            _imagePositionY += _speedY * Time.deltaTime;

            if (_imagePositionY > 1)
                _imagePositionY = 0;

            _image.uvRect = new Rect(0, _imagePositionY, _image.uvRect.width, _image.uvRect.height);

            yield return null;
        }
    }

    public void ChangeSpeed()
    {
        _speedY *= 1.3f;
    }
}
