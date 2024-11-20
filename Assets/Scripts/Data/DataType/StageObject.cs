using System;
using UnityEngine;

[Serializable]
public class StageObject : DataTypeBase
{
    public VectorForSerialize position;
    public VectorForSerialize rotation;
    public VectorForSerialize scale;
}

public struct VectorForSerialize
{
    public float x;
    public float y;
    public float z;

    public VectorForSerialize(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }
}
