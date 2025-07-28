using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShapeData
{
    public ShapeType shape;
    public ShapeColor color;

    public ShapeData(ShapeType s, ShapeColor c)
    {
        shape = s;
        color = c;
    }

    public bool Equals(ShapeData other)
    {
        return this.shape == other.shape && this.color == other.color;
    }
}
