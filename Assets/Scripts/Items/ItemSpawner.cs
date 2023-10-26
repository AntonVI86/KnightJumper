using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _items;
    [SerializeField] private Transform[] _pointsOfSpawn;

    private void Start()
    {
        int index = Random.Range(0, _items.Length);

        for (int i = 0; i < _pointsOfSpawn.Length; i++)
        {
            Instantiate(_items[index], _pointsOfSpawn[i]);
        }
    }
}
