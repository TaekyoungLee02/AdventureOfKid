using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Does Nothing
public class ObjectSerializer : MonoBehaviour
{
    [SerializeField]
    private int id;

    public int ID { get { return id; } set { id = value; } }

    public StageObject Serialize()
    {
        StageObject data = new();

        data.id = id;
        data.position = new VectorForSerialize(transform.localPosition);
        data.rotation = new VectorForSerialize(transform.localRotation.eulerAngles);
        data.scale = new VectorForSerialize(transform.localScale);

        return data;
    }
}
