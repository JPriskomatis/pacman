using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreNumber;

    [SerializeField] private TextMeshProUGUI titleNumber;
    [SerializeField] ScoreManager scoreManager;

    public FloatVariable finalScore;


    private void OnEnable()
    {
        SetScoreNumber(scoreManager.GetScore());
        SetTitle(scoreManager.GetTitle());
    }
    public void SetScoreNumber(float score)
    {
        scoreNumber.text = score.ToString();
        
    }
    public void SetTitle(string title)
    {
        titleNumber.text = "title: "+title.ToString();
    }
    public void RestartLevel()
    {
        finalScore.value = 0;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

    }

    public void WinLevelRestart()
    {

        //Increase the chase chance by 20%
        EnemyMovement.chaseChance += 0.20f;
        FloatVariable.savedValue = scoreManager.GetScore();
        StringVariable.savedValue = scoreManager.GetTitle();

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

        scoreManager.SetScore(FloatVariable.savedValue);
        //scoreManager.SetTitle(StringVariable.savedValue);


    }
}
