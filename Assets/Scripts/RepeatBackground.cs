using UnityEngine;

namespace KaizoTrap
{
    public class RepeatBackground : MonoBehaviour
    {
        private float startPos, length;
        public new GameObject camera;
        public float parallaxEffect;

        void Start()
        {
            startPos = transform.position.y;
            length = GetComponent<SpriteRenderer>().bounds.size.y;
        }

        void Update()
        {
            float temp = camera.transform.position.y * (1 - parallaxEffect);
            float dist = camera.transform.position.y * parallaxEffect;

            transform.position = new Vector3(transform.position.x, startPos + dist, transform.position.z);

            if (temp > startPos + length)
                startPos += length;
            else if (temp < startPos - length)
                startPos -= length;
        }
    }
}
