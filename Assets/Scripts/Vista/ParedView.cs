using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedView : MonoBehaviour
{
    private WallsGenerator generator;
    private GameObject[] paredes;
    
    void Start()
    {
        generator = new WallsGenerator();

        paredes = new GameObject[]
        {
            generator.CreateLeftWall(),
            generator.CreateRightWall(),
            generator.CreateFrontWall(),
            generator.CreateBackWall(),
            generator.CreateRoof(),
            generator.CreateFloor()
        };        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
