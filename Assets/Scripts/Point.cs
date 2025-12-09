using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private Sprite[] logos;

    private void Start()
    {
        int randomLogo = Random.Range(0, logos.Length);
        image.sprite = logos[randomLogo];
    }
}
