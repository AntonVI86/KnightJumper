using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyDamagerView : MonoBehaviour
{
    [SerializeField] private Color _targetColor;

    private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;
    private Enemy _enemy;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _enemy.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        _enemy.Damaged -= OnDamaged;
    }

    private void Start()
    {
        _defaultColor = _spriteRenderer.color;
    }

    private void OnDamaged()
    {       
        StartCoroutine(FadeInColor());
    }

    private IEnumerator FadeInColor()
    {
        float duration = 2f;

        for (float i = 0.01f; i < duration; i += 0.1f)
        {
            _spriteRenderer.material.color = Color.Lerp(_defaultColor, _targetColor, i / duration);
            yield return null;
        }

        StartCoroutine(FadeOutColor());
    }

    private IEnumerator FadeOutColor()
    {
        float duration = 1f;
        float startValue = 0.01f;
        float step = 0.1f;

        for (float i = startValue; i < duration; i += step)
        {
            float normalizedTime = i / duration;

            _spriteRenderer.material.color = Color.Lerp(_targetColor, _defaultColor, normalizedTime);

            yield return null;
        }
    }
}
