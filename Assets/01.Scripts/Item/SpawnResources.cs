using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnResources : MonoBehaviour
{
    public GameObject[] Resources;

    private void Start()
    {
        for (int j = 0; j < 150; j++)
        {
            Instantiate(Resources[0]);        
        }

        for (int j = 0; j < 75; j++)
        {
            Instantiate(Resources[1]);
        }

        for (int j = 0; j < 50; j++)
        {
            Instantiate(Resources[2]);
        }

        for (int j = 0; j < 25; j++)
        {
            Instantiate(Resources[3]);
        }

        for (int j = 0; j < 90; j++)
        {
            Instantiate(Resources[4]);
        }

        //for (int i = 0; i < 10; i++)
        //{
        //    Instantiate(Resources[2]);
        //}
        //for (int i = 0; i < 5; i++)
        //{
        //    Instantiate(Resources[3]);
        //}
    }
}
