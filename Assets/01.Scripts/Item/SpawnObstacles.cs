using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject[] Obstacles;

    private void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(Obstacles[0]);
        }
    }
}
