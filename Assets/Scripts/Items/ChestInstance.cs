using UnityEngine;

public class ChestInstance : MonoBehaviour
{
    [SerializeField] private Chest _chestPrefab;

    private float _chance = 0.5f;

    private void OnEnable()
    {
        float randomChance = Random.Range(0.01f, 1f);

        if(randomChance <= _chance)
        {
            Instantiate(_chestPrefab, transform);
        }
    }
}
