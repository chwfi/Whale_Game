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
    [SerializeField] private TextMeshProUGUI[] _inventoryCountText;
    [SerializeField] private GameObject _arm;
    [SerializeField] private TextMeshProUGUI _fuelPercent;
    [SerializeField] private TextMeshProUGUI _durabilityPercent;
    [SerializeField] private GameObject _panel;
    public Image FuelSlider;
    public Image DurabilitySlider;
    public Slider SpeedSlider;
    public Image OxygenSlider;
    public Image HpSlider;
    public Image ManaSlider;

    [Header("ProductUI")]
    [SerializeField] private TextMeshProUGUI[] _countTexts;
    [SerializeField] private TextMeshProUGUI _resourceWarnText;
    [SerializeField] private Image _productPanel01;
    [SerializeField] private Image _productPanel02;
    [SerializeField] private Image _productPanel03;

    [Header("FuelUI")]
    [SerializeField] private TextMeshProUGUI _fuelCountText;

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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isShowing)
            {
                //OffInfo(_text, _name);
                _arm.SetActive(true);
                Init();
            }
            else
            {
                _arm.SetActive(false);
                _inventoryPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(-5, 180, 0)), 0.25f).OnComplete(() =>
                {
                    isShowing = true;
                    _panel.gameObject.SetActive(true);
                });
            }
        }

        _inventoryCountText[0].text = InventoryManager.Instance.CooperCount.ToString();
        _inventoryCountText[1].text = InventoryManager.Instance.TitanumCount.ToString();
        _inventoryCountText[2].text = InventoryManager.Instance.CooperIngotCount.ToString();
        _inventoryCountText[3].text = InventoryManager.Instance.TitanumIngotCount.ToString();
        _inventoryCountText[4].text = InventoryManager.Instance.SolutionCount.ToString();
        _inventoryCountText[5].text = InventoryManager.Instance.PlasticBottleCount.ToString();
        _inventoryCountText[6].text = InventoryManager.Instance.FuelCount.ToString();
        _inventoryCountText[7].text = InventoryManager.Instance.FishCount.ToString();

        SetProductCountUI();
    }

    public void ShowProductPanel01()
    {
        _productPanel01.gameObject.SetActive(true);
        _productPanel02.gameObject.SetActive(false);
        _productPanel03.gameObject.SetActive(false);
    }

    public void ShowProductPanel02()
    {
        _productPanel01.gameObject.SetActive(false);
        _productPanel02.gameObject.SetActive(true);
        _productPanel03.gameObject.SetActive(false);
    }

    public void ShowProductPanel03()
    {
        _productPanel01.gameObject.SetActive(false);
        _productPanel02.gameObject.SetActive(false);
        _productPanel03.gameObject.SetActive(true);
    }

    private void Init()
    {
        isShowing = false;
        _panel.gameObject.SetActive(false);
        _inventoryPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(-115, 180, 0)), 0.25f);
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

    public void ShowInventoryUI()
    {
        _arm.SetActive(false);
        _controller.CanMove = false;
        _inventoryPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(-10, 180, 0)), 0.45f).OnComplete(() =>
        {
            isShowing = true;
        });
    }

    public void DisableInventoryUI()
    {
        _arm.SetActive(true);
        _controller.CanMove = true;
        isShowing = false;
        _inventoryPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(-115, 180, 0)), 0.45f);
    }

    public void SetProductCountUI()
    {
        _countTexts[0].text = InventoryManager.Instance.CooperCount.ToString() + "/3";
        _countTexts[1].text = InventoryManager.Instance.TitanumCount.ToString() + "/3";
        _countTexts[2].text = InventoryManager.Instance.CooperIngotCount.ToString() + "/2";
        _countTexts[3].text = InventoryManager.Instance.TitanumIngotCount.ToString() + "/1";
        _countTexts[4].text = InventoryManager.Instance.SolutionCount.ToString() + "/2";
        _countTexts[5].text = InventoryManager.Instance.PlasticBottleCount.ToString() + "/1";
    }

    public void ShowProductWarnText(bool value)
    {
        _resourceWarnText.gameObject.SetActive(value);
    }

    public void ShowFuelCountUI(int count)
    {
        _fuelCountText.text = "연료 넣기 " + count.ToString() + "/1";
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
