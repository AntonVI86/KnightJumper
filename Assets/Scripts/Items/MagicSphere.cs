using System.Collections;
using UnityEngine;

public class MagicSphere : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private CircleCollider2D _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PointSetter point))
        {
            _rigidbody.gravityScale = 0;
            _collider.enabled = false;
            StartCoroutine(ChangePosition(point));
        }
    }

    private IEnumerator ChangePosition(PointSetter point)
    {
        while (transform.position != point.SetTarget().position)
        {
            transform.position = Vector2.MoveTowards(_rigidbody.position, point.SetTarget().position, Time.deltaTime * 2f);
            yield return null;
        }

        gameObject.SetActive(false);
        point.ActivateBoss();
    }
}