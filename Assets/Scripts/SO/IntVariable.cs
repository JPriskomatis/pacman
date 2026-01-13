using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "Scriptable Objects/IntVariable")]
public class IntVariable : ScriptableObject
{
    public float value;
    public float originalValue;

    private void OnEnable()
    {
        if (value != originalValue)
        {
            value = originalValue;
        }
    }
}
