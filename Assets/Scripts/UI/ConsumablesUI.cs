using TMPro;
using UnityEngine;

public class ConsumablesUI : MonoBehaviour
{
    [SerializeField] private FloatVariable numberOfConsumables;

    [SerializeField] private TextMeshProUGUI consumablesText;

    private void Start()
    {
        consumablesText.text = numberOfConsumables.value.ToString();
    }

    public void SetBulletsText()
    {
        consumablesText.text = numberOfConsumables.value.ToString();
    }
}
