using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnResources : MonoBehaviour
{
    public GameObject[] Resources;

    private void Start()
    {
        for (int j = 0; j < 75; j++)
        {
            Instantiate(Resources[0]);        
        }

        for (int j = 0; j < 55; j++)
        {
            Instantiate(Resources[1]);
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
