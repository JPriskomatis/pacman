using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="SO/Ghosts")]
public class EnemySO : ScriptableObject
{
    public Sprite[] up;
    public Sprite[] down;
    public Sprite[] left;
    public Sprite[] right;

    public Sprite[] afraid;
    public Sprite[] afraidEnd;
}
