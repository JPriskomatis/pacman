using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scorePoints;
    [SerializeField] private TextMeshProUGUI scoreTitle;

    private int titleIndex;

    [SerializeField] private FloatVariable points;

    private int titleCap = 25;

    private void Start()
    {
        points.value = 0;
        titleIndex = 0;
    }

    private string[] titles = { "Internship Position", "Junior Developer", "Mid-level Developer", "Senior Developer", "Lead Developer", "Principal Engineer" };

    public void IncreaseScore(float scoreAmount)
    {
        points.value += scoreAmount;
        scorePoints.text = points.value.ToString();

        if(points.value > titleCap)
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

    public float GetScore()
    {
        return points.value;
    }
    public string GetTitle()
    {
        return scoreTitle.text;
    }
 }
