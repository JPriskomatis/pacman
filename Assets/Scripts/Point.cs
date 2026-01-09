using UnityEngine;

public class Point : MonoBehaviour, Iinteractable
{
    public GameEvent CollectPoint;
    public GameEvent IncreaseTitle;

    [SerializeField] private AudioClip clip;

    [SerializeField] FloatVariable pointScore;

    public GameEvent RestartLevel;

    int currentPoints = 0;
    public void Interact()
    {
        AudioManager.instance.PlayAudioInstance(clip);
        CollectPoint.Raise();
        pointScore.value++;

        currentPoints++;
        if(currentPoints == 225)
        {
            Debug.Log("End Game");
            RestartLevel.Raise();
        }

        Destroy(gameObject);
    }

    
}
