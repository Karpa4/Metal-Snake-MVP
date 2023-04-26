using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker
{
    public Vector3 Position;
    public Quaternion Rotation;

    public Marker(Vector3 pos, Quaternion rot)
    {
        Position = pos;
        Rotation = rot;
    }
}
