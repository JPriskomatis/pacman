using UnityEngine;

public class Point : MonoBehaviour, Iinteractable
{
    public GameEvent CollectPoint;
    public GameEvent IncreaseTitle;

    [SerializeField] private AudioClip clip;
    public void Interact()
    {
        AudioManager.instance.PlayAudioInstance(clip);
        CollectPoint.Raise();
        Destroy(gameObject);
    }

    
}
