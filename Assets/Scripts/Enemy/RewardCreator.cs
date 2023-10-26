using System.Collections.Generic;
using UnityEngine;

public class RewardCreator : MonoBehaviour
{
    [SerializeField] private List<Coin> _coinPrefab = new List<Coin>();

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _enemy.Died += OnDied;
    }

    private void OnDisable()
    {
        _enemy.Died -= OnDied;
    }

    private void OnDied()
    {
        int index = Random.Range(0, _coinPrefab.Count);
        Instantiate(_coinPrefab[index], _enemy.transform.position, Quaternion.identity);
    }
}
