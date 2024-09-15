using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int count = 5;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject mainTextPanel;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void OpenMenu()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            pausePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
            mainTextPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void ContinueGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            pausePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
            mainTextPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
