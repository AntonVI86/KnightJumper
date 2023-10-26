using UnityEngine;

public class ParallaxControl : MonoBehaviour
{
    [SerializeField] private Transform[] layers;
    [SerializeField] private float[] coeff;

    private void Update()
    {
        for (int i = 0; i < layers.Length; i++) 
        {
            layers[i].position = transform.position * coeff[i];
        }
    }
}
