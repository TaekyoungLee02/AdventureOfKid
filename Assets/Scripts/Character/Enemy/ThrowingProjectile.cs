using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    [SerializeField] private Collider myCollider;

    private List<Collider> alreadyCollider = new List<Collider>();

    public Collider MyCollider { set { myCollider = value; } }

    private void Start()
    {
        Invoke("DestroySelf", 5f);
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;
        if (alreadyCollider.Contains(other)) return;

        alreadyCollider.Add(other);

        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakePhysicalDamage(1);
            EffectManager.Instance.PlayEffect("Hit", 1f, transform.position + Vector3.up, Quaternion.identity, "coin");
        }
    }
}
