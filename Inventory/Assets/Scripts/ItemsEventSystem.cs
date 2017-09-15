using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemsEventSystem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {

    Sprite Itemicon;
    public ItemDB.Item _Item;
    public ItemDB databasereference;

    public SlotEventSystem slot;

    Vector3 initialPosition;

    public void setItem(ItemDB.Item newItem) {

        _Item = newItem;
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + _Item.Slug);
        if (_Item.Stackable)
        {
            slot.StackCounterText.enabled = true;   // aktiviert counter text falls ein stack vorhanden ist
            slot.StackItem(1);
        }
        else
            slot.StackCounterText.enabled = false;
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_Item != null)
        {
            GetComponent<Image>().raycastTarget = false;
            initialPosition = transform.position;
            Inventory.instance.draggingItem = this;
            this.transform.position = eventData.position;
            this.transform.SetParent(Inventory.instance.DragPanel.transform);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_Item != null)
        {
            this.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true; 
        Inventory.instance.draggingItem = null;
        if (Inventory.instance.slotUnderPointer.slotItem == null)   //inventory instance ist eine referenz. 
        {
            int oldstackcounter = slot.stackcounter;
            slot.slotItem = null;
            slot.stackcounter = 0;
            slot.StackCounterText.enabled = false;


            slot = Inventory.instance.slotUnderPointer;     // slot wird verändert
            slot.StackCounterText.enabled = true;
            slot.StackItem(oldstackcounter);
            
            slot.slotItem = this;    // ich sage dem slot wo ich jetzt drauf komme, ich bin dein neues item
            transform.position = slot.transform.position;   //transform der position
            transform.SetParent(slot.transform);    // neu parenten damit der raycast ausgeführt werden kann

        }
        else {
            transform.position = initialPosition;
            this.transform.SetParent(slot.transform); 
        }
        

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_Item != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (slot.stackcounter == 1 || !_Item.Stackable)
            {
                setItem(databasereference.Upgradeitem(_Item));      //Debug.Log("_Item:  "+(_Item == null)+" databasereference: "+ (databasereference==null));
                Debug.Log(_Item.Title + " has been upgraded son");
            }

            else if (slot.stackcounter > 1)
            {
                ItemDB.Item upgradedItem = databasereference.Upgradeitem(_Item);
                if (upgradedItem != _Item)
                {
                    slot.StackItem(-1);
                    Inventory.instance.AddItem(databasereference.Upgradeitem(_Item));   // Upgraded das Item aus dem Stack und fügt es in einen neuen Slot hinzu
                }
            }
           
        }

        if (_Item != null && eventData.button == PointerEventData.InputButton.Left)
        {

        }
    }
}
