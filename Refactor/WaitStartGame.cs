using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitStartGame : MonoBehaviour
{
    private float time = 2.1f;

    private void Awake()
    {
        StartCoroutine(StartCooldown());
    }

    IEnumerator StartCooldown()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
    }
}
