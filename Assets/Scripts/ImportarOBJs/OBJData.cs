using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OBJData
{
    public string name;
    public Vector3 position;
    public Vector3 rotation; // en grados
    public Vector3 scale;

    public OBJData(string name, Vector3 pos, Vector3 rot, Vector3 scale)
    {
        this.name = name;
        position = pos;
        rotation = rot;
        this.scale = scale;
    }
}
