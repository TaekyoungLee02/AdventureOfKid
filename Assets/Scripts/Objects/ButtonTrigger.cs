using System.Collections.Generic;
using UnityEngine;


public class ButtonTrigger : MonoBehaviour
{
    public Transform buttonTransform;
    public List<GameObject> targetObjects;

    List<ITriggerable> targetScripts;

    private void Start()
    {
        targetScripts = new List<ITriggerable>();

        foreach (GameObject obj in targetObjects)
        {
            ITriggerable script = obj.GetComponent<ITriggerable>();
            if (script != null)
            {
                targetScripts.Add(script);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Button"))
        {
            foreach (ITriggerable script in targetScripts)
            {
                script.ExecuteFunction();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            foreach (ITriggerable script in targetScripts)
            {
                script.RevokeFunction();
            }
        }
    }

}

