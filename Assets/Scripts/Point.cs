using UnityEngine;

public class Point : MonoBehaviour, Iinteractable
{
    public GameEvent CollectPoint;
    public void Interact()
    {
        Debug.Log("+1");
        CollectPoint.Raise();
        Destroy(gameObject);
    }

    
}
