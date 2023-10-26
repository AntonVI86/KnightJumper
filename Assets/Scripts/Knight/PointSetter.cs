using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSetter : MonoBehaviour
{
    [SerializeField] private Transform _spherePointTarget;
    [SerializeField] private GameObject _boss;

    public Transform SetTarget()
    {
        return _spherePointTarget;
    }

    public void ActivateBoss()
    {
        _boss.SetActive(true);
    }
}
