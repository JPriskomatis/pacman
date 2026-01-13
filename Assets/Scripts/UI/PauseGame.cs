using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private bool isPause = false;

    

    public void StartPause()
    {
        isPause = !isPause;
        pausePanel.SetActive(isPause);
        if(isPause)
        {
            Time.timeScale = 0f;
        } else
        {
            Time.timeScale = 1f;
        }
    }
}
