using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class HorizontalParallax : MonoBehaviour
{
    [SerializeField] KnightAbilities _knight;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - _knight.transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _knight.transform.position + offset, Time.deltaTime * 2f);
    }
}
