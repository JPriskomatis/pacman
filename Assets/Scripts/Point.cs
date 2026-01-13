using UnityEngine;

public class Point : MonoBehaviour, Iinteractable
{
    public GameEvent CollectPoint;
    public GameEvent IncreaseTitle;

    [SerializeField] private AudioClip clip;

    [SerializeField] FloatVariable pointScore;

    public GameEvent WinLevel;

    [SerializeField] private IntVariable currentPoints;
    public void Interact()
    {
        AudioManager.instance.PlayAudioInstance(clip);
        CollectPoint.Raise();

        currentPoints.value++;
        if(currentPoints.value == 205)
        {
            Debug.Log("End Game");
            WinLevel.Raise();
        }

        Destroy(gameObject);
    }

    
}
