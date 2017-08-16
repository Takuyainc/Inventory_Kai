﻿using System.Collections;
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

    public void setItem( ItemDB.Item newItem) {

        _Item = newItem;
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + _Item.Slug);

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
        if (Inventory.instance.slotUnderPointer.slotItem == null)
        {
            slot.slotItem = null;
            slot = Inventory.instance.slotUnderPointer;
            Inventory.instance.slotUnderPointer.slotItem = this;
            transform.position = Inventory.instance.slotUnderPointer.transform.position;
            transform.SetParent(Inventory.instance.slotUnderPointer.transform);
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
            setItem( databasereference.Upgradeitem(_Item));      //Debug.Log("_Item:  "+(_Item == null)+" databasereference: "+ (databasereference==null));
            Debug.Log(_Item.Title + "has been upgraded son"); 
        }

        if (_Item != null && eventData.button == PointerEventData.InputButton.Left)
        {

        }
    }
}
