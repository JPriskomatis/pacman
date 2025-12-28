using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scorePoints;
    [SerializeField] private TextMeshProUGUI scoreTitle;

    private int titleIndex;

    private int points;

    private int titleCap = 25;

    private void Start()
    {
        points = 0;
        titleIndex = 0;
    }

    private string[] titles = { "Internship Position", "Junior Developer", "Mid-level Developer", "Senior Developer", "Lead Developer", "Principal Engineer" };

    public void IncreaseScore(int scoreAmount)
    {
        points += scoreAmount;
        scorePoints.text = points.ToString();

        if( points > titleCap)
        {
            ChangeScoreTitle();
            titleCap += 40;
        }
    }
    public void ChangeScoreTitle()
    {
        if (titleIndex >= titles.Length)
            return;

        scoreTitle.text = titles[titleIndex];
        titleIndex++;
    }

    public int GetScore()
    {
        return points;
    }
    public string GetTitle()
    {
        return scoreTitle.text;
    }
 }
