using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnResources : MonoBehaviour
{
    public GameObject[] Resources;

    private void Start()
    {
        for (int j = 0; j < 450; j++)
        {
            Instantiate(Resources[0]);        
        }

        for (int j = 0; j < 250; j++)
        {
            Instantiate(Resources[1]);
        }

        for (int j = 0; j < 150; j++)
        {
            Instantiate(Resources[2]);
        }

        for (int j = 0; j < 125; j++)
        {
            Instantiate(Resources[3]);
        }

        for (int j = 0; j < 125; j++)
        {
            Instantiate(Resources[4]);
        }

        for (int j = 0; j < 100; j++)
        {
            Instantiate(Resources[5]);
        }

        for (int j = 0; j < 150; j++)
        {
            Instantiate(Resources[6]);
        }

        for (int j = 0; j < 40; j++)
        {
            Instantiate(Resources[7]);
        }

        for (int j = 0; j < 100; j++)
        {
            Instantiate(Resources[8]);
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
