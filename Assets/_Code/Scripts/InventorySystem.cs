using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private int _inventorySize = 1;

    private Item[] _inventory;

    private int _currentIndex = 0;

    public Item CurrentItem
    {
        get { return _inventory[_currentIndex]; }
        private set { _inventory[_currentIndex] = value; }
    }

    [SerializeField] private Transform _hand;

    private void Awake()
    {
        _inventory = new Item[_inventorySize];
    }

    public Item UseCurrentItem()
    {
        if (CurrentItem == null)
            return null;

        Item tempCurrentItem = CurrentItem;

        if(tempCurrentItem.OneTimeuse)
        {
            RemoveFromInventory();
            tempCurrentItem.gameObject.SetActive(false);
        }

        return tempCurrentItem;
    }

    public void Pickup(Item newItem)
    {
        if (CurrentItem != null)
        {
            for (int slot = 0; slot < _inventory.Length; slot++)
            {
                if (_inventory[slot] != null)
                {
                    PickupItem(newItem, slot);
                }
                else
                {
                    Replace(newItem);
                }
            }
        }
        else
        {
            PickupItem(newItem, _currentIndex);
        }
    }

    private void PickupItem(Item newItem, int slot)
    {
        _inventory[slot] = newItem;
        _currentIndex = slot;
        newItem.transform.SetParent(_hand);
        newItem.transform.localPosition = Vector3.zero;
        CurrentItem.Pickup();
    }

    public void DropCurrentItem()
    {
        if (CurrentItem == null)
            return;

        CurrentItem.Drop();
        CurrentItem.Rigidbody.AddForce(transform.forward * 150);
        RemoveFromInventory();
    }

    public void RemoveFromInventory()
    {
        _inventory[_currentIndex] = null;
    }

    public void Replace(Item newItem)
    {
        CurrentItem.Drop();
        _inventory[_currentIndex] = null;

        PickupItem(newItem, _currentIndex);
    }
}
