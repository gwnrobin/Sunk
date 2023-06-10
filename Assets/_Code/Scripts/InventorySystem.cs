using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private int _inventorySize = 1;

    private Item[] _inventory;

    private int _currentIndex = 0;

    private Item _currentItem
    {
        get { return _inventory[_currentIndex]; }
        set { _inventory[_currentIndex] = value; }
    }

    [SerializeField] Transform _hand;

    private void Awake()
    {
        _inventory = new Item[_inventorySize];
    }

    public void Pickup(Item newItem)
    {
        if (_currentItem != null)
        {
            for (int slot = 0; slot < _inventory.Length; slot++)
            {
                if (_inventory[slot] != null)
                {
                    PickupItem(newItem, slot);
                }
                else
                    Replace(newItem);
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
        _currentItem.Pickup();
    }

    public void DropCurrentItem()
    {
        if (_currentItem == null)
            return;

        _currentItem.Drop();
        _inventory[_currentIndex] = null;
    }

    public void Replace(Item newItem)
    {
        _currentItem.Drop();
        _inventory[_currentIndex] = null;

        PickupItem(newItem, _currentIndex);
    }
}
