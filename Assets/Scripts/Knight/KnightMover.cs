using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KnightMover : KnightAnimator
{
    private KnightAbilities _abilities;
    private Rigidbody2D _rigidbody;
    private KnightAttacker _attacker;

    private void Awake()
    {
        _attacker = GetComponent<KnightAttacker>();
        _abilities = GetComponent<KnightAbilities>();
        _rigidbody = GetComponent<Rigidbody2D>();
        AnimatorPlayer = GetComponent<Animator>();
    }

    public void Move(float horizontal)
    {
        AnimatorStateInfo stateInfo = AnimatorPlayer.GetCurrentAnimatorStateInfo(0);

        _attacker.SetSide(horizontal);
        Vector2 direction = new Vector2(horizontal * _abilities.Speed * Time.fixedDeltaTime, _rigidbody.velocity.y);

        _rigidbody.velocity = direction;

        if(stateInfo.fullPathHash != _jumpHash)
            MoveAnimator(horizontal);
    }

    public void ResetSpeed()
    {
        AnimatorPlayer.SetBool("Walk", false);
        _rigidbody.velocity = Vector2.zero;
    }   
}
