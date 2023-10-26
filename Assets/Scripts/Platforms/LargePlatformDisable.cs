using UnityEngine;

public class LargePlatformDisable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Lava lava))
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
