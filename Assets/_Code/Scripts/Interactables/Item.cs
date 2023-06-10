using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Item : Interactable
{
    private Rigidbody _rb;
    private Collider _col;

    [Header("Throwing")]
    [SerializeField] private int _throwForce = 150;
    [SerializeField] private Vector2 _throwingAngleMultiplier = new(.5f, 1);
    private Vector2 ThrowingAngle => transform.forward * _throwingAngleMultiplier.x + transform.up * _throwingAngleMultiplier.y;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
    }

    public void Pickup()
    {
        _rb.isKinematic = true;
        _col.enabled = false;
    }

    public void Drop()
    {
        transform.parent = null;

        _rb.isKinematic = false;
        _col.enabled = true;

        _rb.AddForce(ThrowingAngle * _throwForce);
    }

    public override void OnInteract()
    {
        base.OnInteract();
        Pickup();
    }
}
