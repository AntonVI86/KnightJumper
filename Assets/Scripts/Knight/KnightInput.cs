using UnityEngine;

[RequireComponent(typeof(KnightMover))]
[RequireComponent(typeof(KnightJumper))]
[RequireComponent(typeof(KnightAttacker))]
public class KnightInput : KnightAnimator
{
    public const string Horizontal = "Horizontal";
    public const string Jump = "Fire2";
    public const string Attack = "Fire1";

    private KnightMover _mover;
    private KnightJumper _jumper;
    private KnightAttacker _attacker;

    private PlayerInput _playerInput;
    private float dir;

    private void Awake()
    {
        AnimatorPlayer = GetComponent<Animator>();
        _mover = GetComponent<KnightMover>();
        _jumper = GetComponent<KnightJumper>();
        _attacker = GetComponent<KnightAttacker>();

        _playerInput = new PlayerInput();
        _playerInput.Player.Attack.performed += ctx => _attacker.Attack(_playerInput.Player.Move.ReadValue<float>());
        _playerInput.Player.Jump.performed += ctx => _jumper.JumpUp();
        _playerInput.Player.Exit.performed += ctx => ExitGame();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void FixedUpdate()
    {
        float direction = _playerInput.Player.Move.ReadValue<float>();

        if (direction > 0)
            dir = dirX;
        else if (direction < 0)
            dir = dirX2;

        _mover.Move(direction);
        AnimatorPlayer.SetFloat("IdleX", dir);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
