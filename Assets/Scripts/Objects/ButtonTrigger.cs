using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public Transform buttonTransform;
    public GameObject targetObject;

    ITriggerable targetScripts;



    private void Start()
    {
        targetScripts = targetObject.GetComponent<ITriggerable>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Button"))
        {
            targetScripts.ExecuteFunction();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            targetScripts.RevokeFunction();
        }
    }

}

