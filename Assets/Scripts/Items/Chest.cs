using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject[] _itemTemplate;
    [SerializeField] private Transform _rewardSpawnedPoint;
    [SerializeField] private AudioClip _openSound;

    private SpriteRenderer _renderer;
    private BoxCollider2D _collider;
    private Animator _animator;
    private Coroutine _coroutine;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Reset();
    }

    public void OpenChest()
    {
        int coinsCount = Random.Range(1, 5);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        SoundPlayer.Instance.PlaySound(_openSound);
        _animator.SetBool("Opened", true);

        _coroutine = StartCoroutine(SceneLoad(coinsCount));     
    }

    private IEnumerator SceneLoad(int coinsCount)
    {
        _collider.enabled = false;

        for (int i = 0; i < coinsCount; i++)
        {
            CreateCoins();

            yield return new WaitForSeconds(0.5f);
        }


        yield return new WaitForSeconds(1f);

        _renderer.DOFade(0, 1f).OnComplete(()=> Destroy(gameObject));
    }

    private void Reset()
    {
        _collider.enabled = true;
        _animator.SetBool("Opened", false);

        if(_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void CreateCoins()
    {
        int index = Random.Range(0, _itemTemplate.Length);

        GameObject spawned = Instantiate(_itemTemplate[index], _rewardSpawnedPoint);
        //spawned.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
        spawned.transform.SetParent(null);
    }
}
