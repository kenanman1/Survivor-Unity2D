using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float waveScaleAnimation = 1.7f;
    [SerializeField] private float waveTextAnimationDuration = 2f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI candyText;
    [SerializeField] private TextMeshProUGUI cashText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI waveCountdownText;
    [SerializeField] private Slider levelSlider;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        waveCountdownText.enabled = false;
    }

    public void UpdatePlayerText(float health, float maxHealth)
    {
        playerHealthText.text = $"Health: {health}/{maxHealth}";
    }

    public void UpdateCandyText(float candy)
    {
        candyText.text = $"Collected: {candy}";
    }

    internal void UpdateCashText(float cashCount)
    {
        cashText.text = $"Collected: {cashCount}";
    }

    public void UpdateLevelSlider(float count)
    {
        levelSlider.value = count;
    }

    public void RestoreLevelSlider(float newMax)
    {
        float newMin = levelSlider.value;
        levelSlider.value = 0;
        levelSlider.minValue = newMin;
        levelSlider.maxValue = newMax;
    }

    public void UpdateLevelText(float level)
    {
        levelText.text = $"Level: {level}";
    }

    public void UpdateWaveText(float wave)
    {
        waveText.text = $"Wave: {wave}";
        LeanTween.scale(waveText.gameObject, new Vector3(waveScaleAnimation, waveScaleAnimation, waveScaleAnimation), waveTextAnimationDuration).setEasePunch();
    }

    public void UpdateWaveCountdownText(float time)
    {
        StartCoroutine(UpdateWaveTimeCoroutine(time));
    }

    private IEnumerator UpdateWaveTimeCoroutine(float time)
    {
        waveCountdownText.enabled = true;

        while (time > 0)
        {
            waveCountdownText.text = $"Next wave in: {time:F1}";
            yield return null;
            time -= Time.deltaTime;
        }

        waveCountdownText.text = "";
        waveCountdownText.enabled = false;
    }
}
