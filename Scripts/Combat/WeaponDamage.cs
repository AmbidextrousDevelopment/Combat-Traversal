using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private int _damage;
    private float _knockback;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }

        if (alreadyCollidedWith.Contains(other)) { return; }

        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(_damage);
        }

        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 force = (other.transform.position - myCollider.transform.position).normalized * _knockback; //create force based on from where enemy hit
            forceReceiver.AddForce(force);
        }
    }

    public void SetAttack(int damage, float knockback)
    {
        _damage = damage;
        _knockback = knockback;
    }
}
