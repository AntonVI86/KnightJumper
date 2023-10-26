using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyCollision : MonoBehaviour
{
    [SerializeField] private Transform[] _pointsOfTeleport;
    [SerializeField] private float _minSpeed = 1f;
    [SerializeField] private float _maxSpeed = 2f;
    [SerializeField] private KnightAbilities _knight;
    [SerializeField] private ParticleSystemRenderer _psr;
    [SerializeField] private AudioClip[] _clips;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    private BoxCollider2D _collider;

    private AudioSource _audio;

    private float _speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
        SetSide();

        StartCoroutine(MovingTiming());
    }

    private void Update()
    {
        _rigidbody.velocity = Vector2.right * _speed;
    }

    public void SetSide()
    {
        if (_speed > 0)
        {
            _renderer.flipX = true;
            _psr.flip = new Vector3(1,0,0);
            _collider.offset = new Vector2(-0.1f, -0.02f);
        }
        else if (_speed < 0)
        {
            _renderer.flipX = false;
            _psr.flip = new Vector3(0, 0, 0);
            _collider.offset = new Vector2(0.08f, -0.02f);
        }
    }

    public void SetSpeed()
    {
        if (transform.position.x < _knight.transform.position.x)
            _speed = 3f;
        else
            _speed = -3f;
    }

    private IEnumerator MovingTiming()
    {
        while (true)
        {            
            yield return new WaitForSeconds(2f);
            int audioIndex = Random.Range(0, _clips.Length);
            int index = Random.Range(0, _pointsOfTeleport.Length);
            transform.position = _pointsOfTeleport[index].position;
            _audio.PlayOneShot(_clips[audioIndex]);
            SetSpeed();
            SetSide();
            yield return new WaitForSeconds(1f);
        }
    }
}
