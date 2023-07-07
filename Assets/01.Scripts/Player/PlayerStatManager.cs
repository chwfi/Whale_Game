using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerStatManager : MonoBehaviour
{
    public static PlayerStatManager Instance;

    [SerializeField] private PlayerSO _playerStats;
    public PlayerSO PlayerStats => _playerStats;

    [SerializeField] private float _currentHp;
    private float _currentMana;
    private float _currentOxygen;
    private float _currentHunger;

    public bool grounded = true;

    [SerializeField] private float _oxDecreaseSpeed = 2f;
    [SerializeField] private float _hpDecreaseSpeed = 4f;
    [SerializeField] private float _manaDecreaseSpeed = 3f;
    [SerializeField] private float _hungerDecreaseSpeed = 3f;
    [SerializeField] private float _increaseSpeed = 10f;

    private DeathEffect _deathEffect;
    [SerializeField] private Image _screenEffect;
 
    public UnityEvent OnDeadTrigger = null;

    private void Awake()
    {
        Instance = this;

        _playerStats.MaxOxygen = 75;
    }

    private void Start()
    {
        _deathEffect = GameObject.Find("Postprocessing").GetComponent<DeathEffect>();

        SetStats(_playerStats.MaxHp, _playerStats.MaxMana, _playerStats.MaxOxygen, _playerStats.MaxHunger);
    }

    public void SetStats(float hp, float mana, float ox, float hunger)
    {
        _currentHp = hp;
        _currentMana = mana;
        _currentOxygen = ox;
        _currentHunger = hunger;
    }

    public void Init()
    {
        _currentHp = _playerStats.MaxHp;
        _currentHunger = _playerStats.MaxHunger;
        _currentMana = _playerStats.MaxMana;
        _currentOxygen = _playerStats.MaxOxygen;
    }

    private void Update()
    {
        if (grounded && _currentHp > 0)
            _deathEffect.canEffect = false;
        if (_currentHp <= 0)
            OnDead();
        SetUI();

        if (_currentHp <= 0) return;

        if (_currentOxygen >= _playerStats.MaxOxygen) _currentOxygen = _playerStats.MaxOxygen;
        if (_currentHp >= _playerStats.MaxHp) _currentHp = _playerStats.MaxHp;

        DecreaseHp();
        DecreaseHunger();
        DecreaseMana();
        DecreaseOxygen();
        IncreaseStats();

        if (grounded)
        {
            if (_currentOxygen >= _playerStats.MaxOxygen) _currentHp += Time.deltaTime * _increaseSpeed;
        }
    }

    private void IncreaseStats()
    {
        //if (_currentOxygen > _playerStats.MaxOxygen) return;
        //if (_currentHp > _playerStats.MaxHp) return;
        if (_currentHunger <= 0 || _currentMana <= 0) return;

        if (grounded && _currentOxygen < _playerStats.MaxOxygen)
        {       
            _currentOxygen += Time.unscaledDeltaTime * _increaseSpeed;
        }
    }

    private void DecreaseOxygen()
    {
        if (_currentOxygen <= 0) return;
        if (grounded) return;

        _currentOxygen -= Time.unscaledDeltaTime * _oxDecreaseSpeed;

        if (_currentOxygen <= 0)
        {
            _deathEffect.canEffect = true;
        }
    }

    private void DecreaseHp()
    {
        if (_currentOxygen <= 0)
        {
            if (grounded) return;
            _currentHp -= Time.unscaledDeltaTime * _hpDecreaseSpeed;
        }       

        if (_currentHunger <= 0 || _currentMana <= 0)
        {
            _deathEffect.canEffect = true;
            _currentHp -= Time.unscaledDeltaTime * _hpDecreaseSpeed;
        }       
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

    public void OnDamage()
    {
        _screenEffect.DOFade(0.1f, 0.2f).SetEase(Ease.OutQuint).OnComplete(() =>
        {
            _screenEffect.DOFade(0, 0.2f);
        });
        _currentHp -= 80f;
    }

    public void IncreaseHunger(float value)
    {
        if (value == 75)
        {
            if (InventoryManager.Instance.FishCount <= 0) return;
            SoundManager.Instance.EatFood();
            _currentHunger += value;
            _currentMana += value / 2;
            InventoryManager.Instance.FishCount -= 1;
        }

        else if (value == 90)
        {
            if (InventoryManager.Instance.WaterGunFishCount <= 0) return;
            SoundManager.Instance.EatFood();
            _currentHunger += value;
            _currentMana += value / 2;
            InventoryManager.Instance.WaterGunFishCount -= 1;
        }
    }

    public void IncreaseMana(float value)
    {
        if (InventoryManager.Instance.WaterCount <= 0) return;
        SoundManager.Instance.DrinkWater();
        _currentMana += value;
        InventoryManager.Instance.WaterCount -= 1;
    }

    private void SetUI()
    {
        UIManager.Instance.SetStatUI(_currentHp, _currentMana, _currentOxygen, _currentHunger);
        UIManager.Instance.SetOxygenText(_currentOxygen);

        if (_currentOxygen <= 25)
        {
            UIManager.Instance.ShowWarningText(1);
            UIManager.Instance.SetWarningText("- 산소 부족", 0);
        }
        else if (_currentOxygen > 25) 
        {
            UIManager.Instance.SetWarningText("", 0);
            if (_currentHunger > 90 && _currentHunger > 100)
                UIManager.Instance.ShowWarningText(0);
        }
        else if (_currentOxygen > 25 && _currentMana > 100 && _currentHunger > 90) UIManager.Instance.ShowWarningText(0);

        if (_currentHunger <= 90)
        {
            UIManager.Instance.ShowWarningText(1);
            UIManager.Instance.SetWarningText("- 허기", 1);
        }        
        else if (_currentHunger > 90)
        {
            UIManager.Instance.SetWarningText("", 1);
            if (_currentMana > 100 && _currentOxygen > 25)
                UIManager.Instance.ShowWarningText(0);
        }
        else if (_currentOxygen > 25 && _currentMana > 100 && _currentHunger > 90) UIManager.Instance.ShowWarningText(0);

        if (_currentMana <= 100)
        {
            UIManager.Instance.ShowWarningText(1);
            UIManager.Instance.SetWarningText("- 갈증", 2);
        }
        else if (_currentMana > 100)
        {
            UIManager.Instance.SetWarningText("", 2);
            if (_currentHunger > 90 && _currentOxygen > 25)
                UIManager.Instance.ShowWarningText(0);
        }
        else if (_currentOxygen > 25 && _currentMana > 100 && _currentHunger > 90) UIManager.Instance.ShowWarningText(0);
    }

    private void OnDead()
    {
        OnDeadTrigger?.Invoke();
    }
}
