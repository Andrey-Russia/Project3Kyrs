using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FloorTrap : MonoBehaviour
{
    [SerializeField] private int _damage = 20;

    private void Reset()
    {
        Collider trapCollider = GetComponent<Collider>();

        if (trapCollider != null)
            trapCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Damageable damageable = other.GetComponentInParent<Damageable>();

        if (damageable == null)
            return;

        damageable.TakeDamage(_damage);

        Debug.Log($"Ловушка нанесла {_damage} урона объекту {other.name}");
    }
}
