using UnityEngine;

public class SmallPlatformDisable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Lava lava))
        {
            if (transform.childCount > 0)
            {
                transform.GetChild(0).SetParent(null);
            }

            gameObject.SetActive(false);

        }
    }
}
