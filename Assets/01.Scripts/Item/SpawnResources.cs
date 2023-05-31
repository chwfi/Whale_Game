using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnResources : MonoBehaviour
{
    public GameObject[] Resources;

    private void Start()
    {
        for (int i = 0; i < 110; i++)
        {
            Instantiate(Resources[3]);     
        }

        for (int i = 0; i < 90; i++)
        {
            Instantiate(Resources[0]);
        }

        for (int j = 0; j < 15; j++)
        {
            Instantiate(Resources[1]);        
        }

        for (int j = 0; j < 25; j++)
        {
            Instantiate(Resources[2]);
        }
    }
}
