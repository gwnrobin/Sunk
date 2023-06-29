using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemInput : InteractableRequireItem
{
    public UnityEvent<Item> PutIn;

    public override void OnInteract(Item item)
    {
        base.OnInteract(item);

        print(item);
    }
}
