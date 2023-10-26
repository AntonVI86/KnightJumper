using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class KnightAbilities : MonoBehaviour
{
    [SerializeField] private ParticleSystem _damageParticle;
    [SerializeField] private LeaderboardView _leader;

    public const string BoxCountString = "BoxCount";

    public List<Ability> _abilities = new List<Ability>();

    private KnightInput _input;
    private const string PathName = "SO";
    private int _defaultMoney = 0;
    private float _defaultSpeed = 50f;
    private float _defaultJumpForce = 6.2f;

    private int _money;
    private int _partsKey;
    private float _speed;
    private float _jumpForce;
    private float _health;
    private float _maxHealth = 3;
    private float _capacity;
    private float _maxCapacity = 1;
    private int _score;
    private int _winScore = 30;

    private BoxCollider2D _collider;

    public event UnityAction Died;
    public event UnityAction<int> MoneyValueChanged;
    public event UnityAction<int> PartsKeyValueChanged;
    public event UnityAction<float> HealthValueChanged;
    public event UnityAction<int> ScoreValueChanged;
    public event UnityAction ScoreReached;
    public event UnityAction WinScoreReached;
    public event UnityAction KeyIsCompleted;
    public event UnityAction<float> BoxTaked;

    public int Money => _money;
    public int PartsKey => _partsKey;
    public float Speed => _speed;
    public float JumpForce => _jumpForce;
    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public float Capacity => _capacity;
    public float MaxCapacity => _maxCapacity;

    public int Score => _score;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _input = GetComponent<KnightInput>();
        Load();
    }
    private void Start()
    {
        _capacity = 0;

        if (PlayerPrefs.HasKey(BoxCountString))
            _capacity = PlayerPrefs.GetFloat(BoxCountString);

        if (PlayerPrefs.HasKey("CurrentHealth") == false)
            _health = _maxHealth;
        else 
            _health = PlayerPrefs.GetFloat("CurrentHealth");

        if (PlayerPrefs.HasKey("WinScore") == false)
            _winScore = PlayerPrefs.GetInt("WinScore");           

        HealthValueChanged?.Invoke(_health);
        MoneyValueChanged?.Invoke(_money);
        ScoreValueChanged?.Invoke(_score);
        PartsKeyValueChanged?.Invoke(_partsKey);

        if (_partsKey == 5)
        {
            WinScoreReached?.Invoke();
        }

        GetAllAbilities();
    }

    public void AddMoney(int coinCost)
    {
        _money += coinCost;
        MoneyValueChanged?.Invoke(_money);
        PlayerPrefs.SetInt("Money", _money);
    }

    public void AddScore(int score)
    {
        int step = 7;

        _score += score;
        ScoreValueChanged?.Invoke(_score);
        PlayerPrefs.SetInt("HighScore", _score);

        if (_score % step == 0)
        {
            ScoreReached?.Invoke();
        }

        //if(_score >= _winScore && _health > 0)
        //{
        //    transform.SetParent(null);

        //    Invoke(nameof(StartWin), 0.1f);           
        //}
    }

    public void AddMaxScore()
    {
        int randomNumber = Random.Range(5, 10);

        _winScore += randomNumber;
        PlayerPrefs.SetInt("WinScore", _winScore);
    }

    private void StartWin()
    {
        WinScoreReached?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        HealthValueChanged?.Invoke(_health);
        _damageParticle.Play();
        PlayerPrefs.SetFloat("CurrentHealth", _health);

        if(_health <= 0)
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
            Agava.YandexGames.Leaderboard.GetPlayerEntry("leader", (result) =>
            {
                if (_score > result.score)
                {
                    _leader.SetScore();
                }
            });
            #endif

            Died?.Invoke();
            _collider.enabled = false;
            _input.enabled = false;
            StopAllCoroutines();
        }
    }

    public void AddPartKey()
    {
        int maxPartsKey = 5;

        _partsKey++;
        PartsKeyValueChanged?.Invoke(_partsKey);

        PlayerPrefs.SetInt("PartsKey", _partsKey);

        if(_partsKey >= maxPartsKey)
        {
            KeyIsCompleted?.Invoke();
        }
    }

    public void AddHealth(float heal)
    {
        if(_health < _maxHealth)
        {
            _health += heal;
            HealthValueChanged?.Invoke(_health);
            PlayerPrefs.SetFloat("CurrentHealth", _health);
        }
    }

    public void TakeOffBox()
    {
        if (_capacity > 0)
            _capacity--;

        PlayerPrefs.SetFloat(BoxCountString, _capacity);

        BoxTaked?.Invoke(_capacity);
    }

    public void AddBox() 
    {
        if (_capacity < _maxCapacity)
            _capacity++;

        PlayerPrefs.SetFloat(BoxCountString, _capacity);
    }

    public void ResetToDefault()
    {
        PlayerPrefs.DeleteAll();

        _money = _defaultMoney;
        _partsKey = 0;
        _capacity = 0;
        _maxCapacity = 1;
        _maxHealth = 3;
        _speed = _defaultSpeed;
        _jumpForce = _defaultJumpForce;
        _winScore = 15;

        GetAllAbilities();

        foreach (var item in _abilities)
        {
            item.ResetLevel();
        }

        Save();
    }

    public void Save()
    {
        if (PlayerPrefs.HasKey("WinScore"))
            _winScore = PlayerPrefs.GetInt("WinScore");

        PlayerPrefs.SetInt("WinScore", _winScore);

        if (PlayerPrefs.HasKey("Money"))
            _money = PlayerPrefs.GetInt("Money");

        PlayerPrefs.SetInt("Money", _money);

        if (PlayerPrefs.HasKey("PartsKey"))
            _money = PlayerPrefs.GetInt("PartsKey");

        PlayerPrefs.SetInt("PartsKey", _partsKey);

        if (PlayerPrefs.HasKey("MaxCapacity"))
            _maxCapacity = PlayerPrefs.GetFloat("MaxCapacity");

        PlayerPrefs.SetFloat("MaxCapacity", _maxCapacity);

        if (PlayerPrefs.GetFloat("Speed") <= _speed)
            PlayerPrefs.SetFloat("Speed", _speed);

        if (PlayerPrefs.GetFloat("JumpForce") <= _jumpForce)
            PlayerPrefs.SetFloat("JumpForce", _jumpForce);

        if (PlayerPrefs.GetFloat("Health") <= _maxHealth)
            PlayerPrefs.SetFloat("Health", _maxHealth);
    }

    public void Load()
    {
        _money = PlayerPrefs.GetInt("Money");
        _partsKey = PlayerPrefs.GetInt("PartsKey");
        _speed = PlayerPrefs.GetFloat("Speed");
        _jumpForce = PlayerPrefs.GetFloat("JumpForce");
        _maxHealth = PlayerPrefs.GetFloat("Health");
        _winScore = PlayerPrefs.GetInt("WinScore");
        _maxCapacity = PlayerPrefs.GetFloat("MaxCapacity");
    }

    public void ResetScore()
    {
        _winScore = 0;
        WinScoreReached?.Invoke();
    }

    private void GetAllAbilities()
    {
        Object[] objects = Resources.LoadAll(PathName, typeof(Ability));

        foreach (var element in objects)
        {
            Ability ability = (Ability)element;
            _abilities.Add(ability);
        }
    }
}
