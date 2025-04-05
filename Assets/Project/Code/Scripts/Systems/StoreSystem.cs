using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class StoreSystem : MonoBehaviour
{
    public static StoreSystem instance;

    [Header("References")]
    [SerializeField] private Life playerLife;
    [SerializeField] private Gun playerGun;
    [SerializeField] private GameObject storePanel;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private GameObject HUD;

    [Header("Cursor")]
    [SerializeField] private Texture2D customCursor;
    [SerializeField] private Vector2 cursorHotspot = Vector2.zero;

    [Header("Store Items")]
    private int healCost = 50;
    private int maxHealthUpgradeCost = 100;
    private int maxShieldUpgradeCost = 100;
    private int restoreMissilesCost = 60;
    private int maxMissilesUpgradeCost = 150;
    private int shieldRegenUpgradeCost = 100;

    [Header("Upgrade Values")]
    private float healthIncreaseAmount = 25f;
    private float shieldIncreaseAmount = 25f;
    private float shieldRegenRateIncrease = 2f;
    private int missilesIncreaseAmount = 1;

    private int currentCoins;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        storePanel.SetActive(false);
        UnityEngine.Cursor.SetCursor(customCursor, cursorHotspot, CursorMode.Auto);
    }

    public void OpenStore()
    {
        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        HUD.SetActive(false);
        storePanel.SetActive(true);
        Time.timeScale = 0f;
        UpdateUI();
    }

    public void CloseStore()
    {
        UnityEngine.Cursor.SetCursor(customCursor, cursorHotspot, CursorMode.Auto);
        storePanel.SetActive(false);
        HUD.SetActive(true);
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
