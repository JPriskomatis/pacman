using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    Vector3 originalPos = new Vector3(6.16f, -0.37f, -10);
    [SerializeField] private float magnituted = 0.5f;
    [SerializeField] private float duration = 1f;



    public void CallScreenShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnituted;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnituted;

            transform.localPosition = new Vector3(6.16f+xOffset, -0.37f + yOffset, originalPos.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
