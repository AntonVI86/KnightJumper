using System.Collections;
using UnityEngine;

public class PlatformGenerator : ObjectPool
{
    [SerializeField] private GameObject[] _templates;

    private float _step = -4.5f;
    private float _minXPosition = -1.8f;
    private float _maxXPosition = 1.5f;

    private void Start()
    {
        Initialize(_templates);

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            float randomStep = Random.Range(1.3f, 2f);

            if(TryGetObject(out GameObject platform))
            {
                _step += randomStep;
                platform.SetActive(true);

                SetPlatformPosition(platform);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SetPlatformPosition(GameObject platform)
    {
        float positionX = Random.Range(_minXPosition, _maxXPosition);
        Vector3 spawnPoint = new Vector3(positionX, _step, platform.transform.position.z);

        platform.transform.position = spawnPoint;
    }
}
