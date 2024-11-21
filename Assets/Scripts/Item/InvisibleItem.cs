using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisableItem : MonoBehaviour
{
    public List<GameObject> Items;
    public List<Renderer> Renderers;

    private void Start()
    {
        Renderers = new List<Renderer>();

        foreach (GameObject obj in Items)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                Renderers.Add(renderer);
                renderer.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for(int i = 0; i < Renderers.Count; i++)
            {
                Renderers[i].enabled = true;
            }
        }
    }
}
