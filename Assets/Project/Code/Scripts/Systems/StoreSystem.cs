using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreSystem : MonoBehaviour
{
    public static StoreSystem instance;

    [Header("References")]
    [SerializeField] private Life playerLife;
    [SerializeField] private Gun playerGun;
    [SerializeField] private GameObject storePanel;
    [SerializeField] private TextMeshProUGUI coinsText;

    [Header("Store Items")]
    [SerializeField] private int healCost = 100;
    [SerializeField] private int maxHealthUpgradeCost = 150;
    [SerializeField] private int maxShieldUpgradeCost = 150;
    [SerializeField] private int restoreMissilesCost = 75;
    [SerializeField] private int maxMissilesUpgradeCost = 200;
    [SerializeField] private int shieldRegenUpgradeCost = 125;

    [Header("Upgrade Values")]
    [SerializeField] private float healthIncreaseAmount = 25f;
    [SerializeField] private float shieldIncreaseAmount = 25f;
    [SerializeField] private float shieldRegenRateIncrease = 2f;
    [SerializeField] private int missilesIncreaseAmount = 1;

    private int currentCoins;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        storePanel.SetActive(false);
    }

    public void OpenStore()
    {
        storePanel.SetActive(true);
        Time.timeScale = 0f;
        UpdateUI();
    }

    public void CloseStore()
    {
        storePanel.SetActive(false);
        Time.timeScale = 1f;
        WaveManager.instance.StartNextWave();
    }

    public void BuyHealthRestore()
    {
        if (CanAfford(healCost))
        {
            SpendCoins(healCost);
            playerLife.life = playerLife.maxLife;
            UpdateUI();
        }
    }

    public void BuyMaxHealthUpgrade()
    {
        if (CanAfford(maxHealthUpgradeCost))
        {
            SpendCoins(maxHealthUpgradeCost);
            playerLife.maxLife += healthIncreaseAmount;
            playerLife.life += healthIncreaseAmount;
            UpdateUI();
        }
    }

    public void BuyMaxShieldUpgrade()
    {
        if (CanAfford(maxShieldUpgradeCost))
        {
            SpendCoins(maxShieldUpgradeCost);
            playerLife.maxShield += shieldIncreaseAmount;
            playerLife.shield += shieldIncreaseAmount;
            UpdateUI();
        }
    }

    public void BuyMissilesRestore()
    {
        if (CanAfford(restoreMissilesCost))
        {
            SpendCoins(restoreMissilesCost);
            playerGun.misiles = playerGun.maxMisiles;
            UpdateUI();
        }
    }

    public void BuyMaxMissilesUpgrade()
    {
        if (CanAfford(maxMissilesUpgradeCost))
        {
            SpendCoins(maxMissilesUpgradeCost);
            playerGun.maxMisiles += missilesIncreaseAmount;
            playerGun.misiles += missilesIncreaseAmount;
            UpdateUI();
        }
    }

    public void BuyShieldRegenUpgrade()
    {
        if (CanAfford(shieldRegenUpgradeCost))
        {
            SpendCoins(shieldRegenUpgradeCost);
            playerLife.shieldRegenRate += shieldRegenRateIncrease;
            UpdateUI();
        }
    }

    private bool CanAfford(int cost)
    {
        return currentCoins >= cost;
    }

    private void SpendCoins(int amount)
    {
        currentCoins -= amount;
    }

    private void UpdateUI()
    {
        coinsText.text = $"Coins: {currentCoins}";
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        UpdateUI();
    }
}
