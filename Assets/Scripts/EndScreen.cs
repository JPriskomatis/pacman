using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreNumber;

    [SerializeField] private TextMeshProUGUI titleNumber;
    [SerializeField] ScoreManager scoreManager;

    private void OnEnable()
    {
        SetScoreNumber(scoreManager.GetScore());
        SetTitle(scoreManager.GetTitle());
    }
    public void SetScoreNumber(int score)
    {
        scoreNumber.text = score.ToString();
        
    }
    public void SetTitle(string title)
    {
        titleNumber.text = "title: "+title.ToString();
    }
    public void RestartLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
