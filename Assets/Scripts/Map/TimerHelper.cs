using System.Collections;
using UnityEngine;

public class TimerHelper : MonoBehaviour
{
    
    public void CallTimerHelp()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(5f);
    }
}
