using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatManager : MonoBehaviour
{
    [SerializeField] private PlayerSO _playerStats;
    public PlayerSO PlayerStats => _playerStats;

    [SerializeField] private float _currentHp;
    private float _currentMana;
    private float _currentOxygen;

    public bool grounded = true;

    [SerializeField] private float _oxDecreaseSpeed = 2f;
    [SerializeField] private float _hpDecreaseSpeed = 4f;
    [SerializeField] private float _manaDecreaseSpeed = 3f;

    [SerializeField] private float _increaseSpeed = 10f;
 
    public UnityEvent OnDeadTrigger = null;

    private void Start()
    {
        SetStats(_playerStats.MaxHp, _playerStats.MaxMana, _playerStats.MaxOxygen);
    }

    public void SetStats(float hp, float mana, float ox)
    {
        _currentHp = hp;
        _currentMana = mana;
        _currentOxygen = ox;
    }

    private void Update()
    {
        if (_currentHp <= 0) return;

        if (_currentOxygen >= _playerStats.MaxOxygen) _currentOxygen = _playerStats.MaxOxygen;
        if (_currentHp >= _playerStats.MaxHp) _currentHp = _playerStats.MaxHp;

        DecreaseHp();
        DecreaseOxygen();
        IncreaseStats();

        SetUI();
    }

    private void IncreaseStats()
    {
        if (_currentOxygen > _playerStats.MaxOxygen) return;
        if (_currentHp > _playerStats.MaxHp) return;

        if (grounded)
        {
            _currentOxygen += Time.unscaledDeltaTime * _increaseSpeed;
            _currentHp += Time.deltaTime * _increaseSpeed;
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
        if (grounded) return;

        if (_currentOxygen <= 0)
            _currentHp -= Time.unscaledDeltaTime * _hpDecreaseSpeed;

        if (_currentHp <= 0)
            OnDead();
    }

    private void SetUI()
    {
        UIManager.Instance.SetStatUI(_currentHp, _currentMana, _currentOxygen);
        UIManager.Instance.SetOxygenText(_currentOxygen);
    }

    private void OnDead()
    {
        Debug.Log("»ç¸Á");
        OnDeadTrigger?.Invoke();
    }
}
