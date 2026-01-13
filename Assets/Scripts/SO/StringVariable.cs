using UnityEngine;

[CreateAssetMenu(fileName = "StringVariable", menuName = "Scriptable Objects/StringVariable")]
public class StringVariable : ScriptableObject
{
    public string value;
    public string originalValue;
    public static string savedValue;

    private void OnEnable()
    {
        if (!value.Equals(originalValue))
        {
            value = originalValue;
        }
    }
}
