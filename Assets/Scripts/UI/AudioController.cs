using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    private bool isMute = false;
    [SerializeField] private Image currentSprite;
    [SerializeField] private Sprite muteSprite, unmuteSprite;

    [SerializeField] private AudioListener audioListener;

    public void SetAudio()
    {
        isMute = !isMute;
        if (isMute)
        {
            currentSprite.sprite = muteSprite;
            audioListener.enabled = false;
        }
        else
        {
            currentSprite.sprite = unmuteSprite;
            audioListener.enabled = true;
        }
    }
}
