using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using StarterAssets;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _outInfo;
    [SerializeField] private TextMeshProUGUI _keyInfo;
    [SerializeField] private TextMeshProUGUI _oxygenText;
    [SerializeField] private CanvasGroup _warnInfo;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private TextMeshPro[] _countText;
    [SerializeField] private TextMeshPro[] _nameText;
    [SerializeField] private GameObject _arm;
    [SerializeField] private TextMeshProUGUI _fuelPercent;
    [SerializeField] private TextMeshProUGUI _durabilityPercent;
    public Image FuelSlider;
    public Image DurabilitySlider;
    public Slider SpeedSlider;
    public Image OxygenSlider;
    public Image HpSlider;
    public Image ManaSlider;

    public bool isShowing = false;

    [Header("Value")]
    [SerializeField] private float _fadeSpeed = 0.8f;

    private FirstPersonController _controller;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple UIManager is running");
        }
        Instance = this;
    }

    private void Start()
    {
        _controller = GameObject.Find("Player").GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        FuelSlider.fillAmount = FuelSystem.Instance.Gauge / FuelSystem.Instance.MaxFuel;
        _fuelPercent.text = (FuelSystem.Instance.Gauge / FuelSystem.Instance.MaxFuel).ToString("P0");

        DurabilitySlider.fillAmount = DurabilitySystem.Instance.CurrentDurability / DurabilitySystem.Instance.MaxDurability;
        _durabilityPercent.text = (DurabilitySystem.Instance.CurrentDurability / DurabilitySystem.Instance.MaxDurability).ToString("P0");

        if (Input.GetKeyDown(KeyCode.Tab) && _controller.CanRotateCam)
        {
            if (isShowing)
            {
                DisableInventoryUI();
            }
            else
            {
                ShowInventoryUI();
            }
        }
    }

    public void SetStatUI(float hp, float mana, float ox)
    {
        HpSlider.fillAmount = hp / 100;
        ManaSlider.fillAmount = mana / 50;
        OxygenSlider.fillAmount = ox / 100;
    }

    public void SetOxygenText(float value)
    {
        _oxygenText.text = (value / 100).ToString("P0");
    }

    public void ShowWarningText(int value)
    {
        _warnInfo.DOFade(value, 1); 
    }

    public void SetInventoryUI(int count, string name, int num)
    {
        _countText[num].text = count.ToString();
        _nameText[num].text = name;
    }

    public void ShowInventoryUI()
    {
        _arm.SetActive(false);
        _controller.CanMove = false;
        _inventoryPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(-10, 180, 0)), 0.75f).OnComplete(() =>
        {
            isShowing = true;
        });
    }

    public void DisableInventoryUI()
    {
        _arm.SetActive(true);
        _controller.CanMove = true;
        isShowing = false;
        _inventoryPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(-115, 180, 0)), 0.75f);
    }

    public void ShowInfo(TextMeshPro text, TextMeshPro name)
    {
        text.DOFade(1, _fadeSpeed);
        name.DOFade(1, _fadeSpeed);
    }

    public void OffInfo(TextMeshPro text, TextMeshPro name)
    {
        text.DOFade(0, _fadeSpeed);
        name.DOFade(0, _fadeSpeed);
    }

    public void InSpaceInfo()
    {
        _outInfo.DOFade(1, _fadeSpeed);
        _keyInfo.DOFade(1, _fadeSpeed);
    }

    public void OffSpaceInfo()
    {
        _outInfo.DOFade(0, _fadeSpeed);
        _keyInfo.DOFade(0, _fadeSpeed);
    }
}
