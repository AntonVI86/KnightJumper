using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Start()
    {
        
    }
    private void Update()
    {
        transform.position = new Vector2(transform.position.x,transform.position.y + _speed* Time.deltaTime); 
    }
}
