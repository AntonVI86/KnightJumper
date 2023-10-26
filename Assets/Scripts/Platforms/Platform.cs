using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private int _score;
    private bool _isTouch;

    private void OnEnable()
    {
        _isTouch = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out KnightAbilities knight))
        {
            if(_isTouch == false)
            {
                knight.AddScore(_score);
                _isTouch = true;
            }
        }
    }
}
