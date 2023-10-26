using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _target.position;
    }
    private void FixedUpdate()
    {
        transform.position = _target.position + _offset;
        transform.rotation = Quaternion.Euler(0,0,0);
    }
}
