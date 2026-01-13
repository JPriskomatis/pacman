using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    private bool isMute = false;
    [SerializeField] private Image currentSprite;
    [SerializeField] private Sprite muteSprite, unmuteSprite;

    [SerializeField] private AudioSource[] sources;

    public void SetAudio()
    {
        isMute = !isMute;
        if (isMute)
        {
            currentSprite.sprite = muteSprite;
            foreach (var source in sources)
            {
                source.mute = true;
            }
        }
        else
        {
            currentSprite.sprite = unmuteSprite;
            foreach (var source in sources)
            {
                source.mute = false;
            }
        }
    }
}
