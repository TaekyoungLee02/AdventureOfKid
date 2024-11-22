using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterManager.Instance.Player.AddItem?.Invoke(itemData);
            Destroy(gameObject);
        }
    }
}
