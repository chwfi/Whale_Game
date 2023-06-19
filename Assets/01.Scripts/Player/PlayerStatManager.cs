using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatManager : MonoBehaviour
{
    public static PlayerStatManager Instance;

    [SerializeField] private PlayerSO _playerStats;
    public PlayerSO PlayerStats => _playerStats;

    [SerializeField] private float _currentHp;
    private float _currentMana;
    [SerializeField] private float _currentOxygen;
    private float _currentHunger;

    public bool grounded = true;

    [SerializeField] private float _oxDecreaseSpeed = 2f;
    [SerializeField] private float _hpDecreaseSpeed = 4f;
    [SerializeField] private float _manaDecreaseSpeed = 3f;
    [SerializeField] private float _hungerDecreaseSpeed = 3f;
    [SerializeField] private float _increaseSpeed = 10f;
 
    public UnityEvent OnDeadTrigger = null;

    private void Awake()
    {
        Instance = this;

        _playerStats.MaxOxygen = 75;
    }

    private void Start()
    {
        SetStats(_playerStats.MaxHp, _playerStats.MaxMana, _playerStats.MaxOxygen, _playerStats.MaxHunger);
    }

    public void SetStats(float hp, float mana, float ox, float hunger)
    {
        _currentHp = hp;
        _currentMana = mana;
        _currentOxygen = ox;
        _currentHunger = hunger;
    }

    private void Update()
    {
        if (_currentHp <= 0) return;

        if (_currentOxygen >= _playerStats.MaxOxygen) _currentOxygen = _playerStats.MaxOxygen;
        if (_currentHp >= _playerStats.MaxHp) _currentHp = _playerStats.MaxHp;

        DecreaseHp();
        DecreaseHunger();
        DecreaseMana();
        DecreaseOxygen();
        IncreaseStats();

        SetUI();
    }

    private void IncreaseStats()
    {
        if (_currentOxygen > _playerStats.MaxOxygen) return;
        if (_currentHp > _playerStats.MaxHp) return;
        if (_currentHunger <= 0 || _currentMana <= 0) return;

        if (grounded)
        {
            _currentOxygen += Time.unscaledDeltaTime * _increaseSpeed;
            if (_currentOxygen >= _playerStats.MaxOxygen) _currentHp += Time.deltaTime * _increaseSpeed;
        }
    }

    private void DecreaseOxygen()
    {
        if (_currentOxygen <= 0) return;
        if (grounded) return;

        _currentOxygen -= Time.unscaledDeltaTime * _oxDecreaseSpeed;
    }

    private void DecreaseHp()
    {
        if (_currentOxygen <= 0)
        {
            if (grounded) return;
            _currentHp -= Time.unscaledDeltaTime * _hpDecreaseSpeed;
        }       

        if (_currentHunger <= 0 || _currentMana <= 0)
            _currentHp -= Time.unscaledDeltaTime * _hpDecreaseSpeed;

        if (_currentHp <= 0)
            OnDead();
    }

    private void DecreaseHunger()
    {
        if (_currentHunger <= 0) return;

        _currentHunger -= Time.unscaledDeltaTime * _hungerDecreaseSpeed;
    }

    private void DecreaseMana()
    {
        if (_currentMana <= 0) return;

        _currentMana -= Time.unscaledDeltaTime * _manaDecreaseSpeed;
    }

    public void IncreaseHunger(float value)
    {
        if (InventoryManager.Instance.FishCount <= 0) return;

        _currentHunger += value;
        _currentMana += value / 2;
        InventoryManager.Instance.FishCount -= 1;
    }

    public void IncreaseMana(float value)
    {
        if (InventoryManager.Instance.WaterCount <= 0) return;

        _currentMana += value;
        InventoryManager.Instance.WaterCount -= 1;
    }

    private void SetUI()
    {
        UIManager.Instance.SetStatUI(_currentHp, _currentMana, _currentOxygen, _currentHunger);
        UIManager.Instance.SetOxygenText(_currentOxygen);

        if (_currentOxygen <= 25)
            UIManager.Instance.ShowWarningText(1);
        else
            UIManager.Instance.ShowWarningText(0);
    }

    private void OnDead()
    {
        Debug.Log("»ç¸Á");
        OnDeadTrigger?.Invoke();
    }
}
