using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class KnightJumper : KnightAnimator
{
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private ParticleSystem _dustParticles;
    [SerializeField] private AudioClip[] _jumpSound;
    [SerializeField] private Vector2 _size;

    private KnightAbilities _abilities;
    private Rigidbody2D _rigidbody;
    private AudioSource _audio;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        AnimatorPlayer = GetComponent<Animator>();
        _abilities = GetComponent<KnightAbilities>();
        _audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        IsGrounded();
    }
    public void JumpUp()
    {
        IsGrounded();
        if (_isGrounded)
        {
            _isGrounded = false;
            JumpAnimator();
            JumpUpPhysics();
        }
    }

    private void JumpUpPhysics()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(Vector2.up * _abilities.JumpForce, ForceMode2D.Impulse);
        int index = Random.Range(0, _jumpSound.Length);
        _dustParticles.Play();
        _audio.PlayOneShot(_jumpSound[index]);
        
    }

    private void IsGrounded()
    {
        Vector2 size = new Vector2(0.53f, 0.01f);
        Collider2D[] coll = Physics2D.OverlapBoxAll(_groundCheckPoint.position, size, 0, _groundLayer);
     
        _isGrounded = coll.Length > 0;
    }
}
