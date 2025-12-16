using UnityEngine;

public class Point : MonoBehaviour, Iinteractable
{
    public GameEvent CollectPoint;
    public GameEvent IncreaseTitle;
    public void Interact()
    {
        
        CollectPoint.Raise();
        Destroy(gameObject);
    }

    
}
