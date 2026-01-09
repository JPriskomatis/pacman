using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour
{
    [SerializeField] private VideoPlayer player;

    private void Start()
    {
        player.url = System.IO.Path.Combine(Application.streamingAssetsPath, "game-over.mp4");

    }
}
