using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject mainTextPanel;
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private GameObject statsUpgrade;
    [SerializeField] private GameObject[] panels;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ShowPanel(mainTextPanel);
        PlayerController.OnPlayerDeathEvent += ShowDeadPanel;
        Player.onLevelUp += ShowWaveTransitionPanel;
    }

    public void StartGame()
    {
        ShowPanel(mainTextPanel);
    }

    public void ShowPanel(GameObject panelToShow)
    {
        foreach (GameObject panel in panels)
        {
            if (panelToShow.name == pausePanel.name)
            {
                panel.SetActive(true);
                panel.GetComponent<CanvasGroup>().blocksRaycasts = true;
                mainTextPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
                break;
            }
            else
            {
                if (panel == panelToShow)
                {
                    panel.SetActive(true);
                    panel.GetComponent<CanvasGroup>().blocksRaycasts = true;
                }
                else
                {
                    panel.SetActive(false);
                    panel.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
            }
        }
    }

    public void OpenPause()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            ShowPanel(pausePanel);
        }
    }

    public void ContinueGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            ShowPanel(mainTextPanel);
        }
    }

    public void ShowDeadPanel()
    {
        WaveManager.instance.StopWaves();
        ShowPanel(deadPanel);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ShowWaveTransitionPanel()
    {
        ShowPanel(statsUpgrade);
        Time.timeScale = 0;
    }
}
