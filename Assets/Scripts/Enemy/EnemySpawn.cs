using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;

    private void OnEnable()
    {
        if (transform.childCount <= 0)
            CreateEnemy();
    }

    private void CreateEnemy()
    {
        int index = Random.Range(0, _enemies.Length);
        GameObject spawned = Instantiate(_enemies[index], transform);
    }
}
