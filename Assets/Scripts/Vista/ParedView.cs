using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedView : MonoBehaviour
{
    private WallsGenerator generator;
    private GameObject paredIzquierda;
    private GameObject paredDerecha;
    private GameObject paredFrontal;
    private GameObject paredTrasera;
    
    void Start()
    {
        generator = new WallsGenerator();
        
        paredIzquierda = generator.CreateLeftWall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
