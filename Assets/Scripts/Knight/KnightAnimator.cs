using UnityEngine;

[RequireComponent(typeof(Animator))]
public class KnightAnimator : MonoBehaviour
{
    private int _attackHash = Animator.StringToHash("Attack");
    protected int _jumpHash = Animator.StringToHash("Jump");
    private int _walkHash = Animator.StringToHash("Walk");
    private int _deadHash = Animator.StringToHash("Death");

    protected Animator AnimatorPlayer;
    protected const float dirX = 1;
    protected const float dirX2 = -1;

    protected void AttackAnimator(float horizontal)
    {
        AnimatorPlayer.SetTrigger(_attackHash);
        AnimatorPlayer.SetFloat("MoveX", horizontal);
    }

    protected void JumpAnimator()
    {
        AnimatorPlayer.SetTrigger(_jumpHash);
        //AnimatorPlayer.SetFloat("MoveX", horizontal);
    }

    protected void MoveAnimator(float speed)
    {
        AnimatorPlayer.SetBool(_walkHash, true);
        AnimatorPlayer.SetFloat("MoveX", speed);

        if (speed == 0)
            AnimatorPlayer.SetBool(_walkHash, false);
    }
}
