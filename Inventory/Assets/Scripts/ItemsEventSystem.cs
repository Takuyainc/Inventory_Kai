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
    public int stacks;


    public void setItem( ItemDB.Item newItem) {

        _Item = newItem;
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + _Item.Slug);

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_Item != null)
        {
            Inventory.instance.draggingItem = this;
            this.transform.position = eventData.position;
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
        Inventory.instance.draggingItem = null;
        transform.position = Inventory.instance.slotUnderPointer.transform.position;
        transform.SetParent(Inventory.instance.slotUnderPointer.transform);
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
