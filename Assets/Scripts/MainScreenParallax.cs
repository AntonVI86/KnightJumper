using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenParallax : MonoBehaviour
{
    [SerializeField] private RectTransform[] _grounds;
    [SerializeField] private float[] _speed;

    private void Start()
    {
        StartCoroutine(MoveScreens());
    }

    private IEnumerator MoveScreens()
    {
        while (true)
        {
            for (int i = 0; i < _grounds.Length; i++)
            {
                _grounds[i].anchoredPosition += new Vector2(_speed[i], 0);

                if(_grounds[i].anchoredPosition.x > 70f || _grounds[i].anchoredPosition.x < -17f)
                {
                    _speed[i] *= -1;
                }
                yield return null;
            }
        }
    }
}
