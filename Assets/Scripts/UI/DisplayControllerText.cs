using TMPro;
using UnityEngine;

public class DisplayControllerText : MonoBehaviour
{
    public static DisplayControllerText Instance;
    [SerializeField] private TextMeshProUGUI textComponent;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetDisplayText(string textToDisplay)
    {
        textComponent.gameObject.SetActive(true);
        textComponent.text = textToDisplay;
        Invoke("DisableObject", 3f);
    }

    public void DisableObject()
    {
        textComponent.gameObject.SetActive(false);
    }
}
