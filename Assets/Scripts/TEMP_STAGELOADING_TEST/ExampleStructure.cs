using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleStructure : MonoBehaviour, IDataInitializer
{
    public int a;
    public int b;

    public void Initialize(List<int> values)
    {
        a = values[0];
        b = values[1];
    }
}
