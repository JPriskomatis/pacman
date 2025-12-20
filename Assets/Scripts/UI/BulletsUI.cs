using TMPro;
using UnityEngine;

public class BulletsUI : MonoBehaviour
{
    [SerializeField] private FloatVariable numberOfBullets;

    [SerializeField] private TextMeshProUGUI bulletsText;

    private void Start()
    {
        bulletsText.text = numberOfBullets.value.ToString();
    }

    public void SetBulletsText()
    {
        bulletsText.text = numberOfBullets.value.ToString();
    }
}
