using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float coinDropChance = 0.3f;

    private int currentScore = 0;
    private int currentWave = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
    }

    public void AddWave(int amount)
    {
        currentWave = amount;
        UpdateScoreUI();
    }

    public void TryDropCoin(Vector3 position)
    {
        if (Random.value <= coinDropChance)
        {
            GameObject coin = Instantiate(coinPrefab, position, Quaternion.identity);
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
        }
        if (waveText != null)
        {
            waveText.text = $"Max Wave: {currentWave}";
        }
    }
}
