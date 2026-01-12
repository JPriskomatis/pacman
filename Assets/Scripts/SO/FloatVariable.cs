using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "Scriptable Objects/FloatVariable")]
public class FloatVariable : ScriptableObject
{
    public float value;
    public float originalValue;
    public static float savedValue;

    private void OnEnable()
    {
        if (value != originalValue)
        {
            value = originalValue;
        }
    }
}
