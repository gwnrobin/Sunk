using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Item : Interactable
{
    public Rigidbody Rigidbody => _rb;
    public bool OneTimeuse => _oneTimeuse;

    [SerializeField] private bool _oneTimeuse;

    private Rigidbody _rb;
    private Collider _col;

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
    }

    public override void OnInteract()
    {
        base.OnInteract();
        Pickup();
    }
}
