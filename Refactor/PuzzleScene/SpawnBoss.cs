using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject boss;

    private void Start()
    {
        Instantiate(boss, new Vector3(18, -16, 0), Quaternion.identity);
    }
}
