using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject Obstacles;

    private void Start()
    {
        for (int j = 0; j < 12; j++)
        {
            Instantiate(Obstacles);
        }
    }
}
