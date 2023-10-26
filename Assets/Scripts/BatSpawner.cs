using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _template;
    [SerializeField] private Transform[] _spawnPoint;

    private float _timeBetweenSpawn;
    private float _minTime = 5f;
    private float _maxTime = 15f;

    private Coroutine _coroutine;

    private void Start()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Create());
    }


    private IEnumerator Create()
    {
        while (true)
        {
            _timeBetweenSpawn = Random.Range(_minTime, _maxTime);
            GameObject newBat = Instantiate(_template, _spawnPoint[Random.Range(0, _spawnPoint.Length - 1)]);
            newBat.GetComponent<SpriteRenderer>().flipX = true;
            yield return new WaitForSeconds(_timeBetweenSpawn);
        }
    }
}
