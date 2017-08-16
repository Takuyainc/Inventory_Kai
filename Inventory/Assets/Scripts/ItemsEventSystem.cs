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
    public int stacks;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_Item != null)
        {
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

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_Item != null && eventData.button == PointerEventData.InputButton.Right)
        {
            _Item = databasereference.Upgradeitem(_Item, databasereference.playerMoney);      //Debug.Log("_Item:  "+(_Item == null)+" databasereference: "+ (databasereference==null));
            Debug.Log(_Item.Title + "has been upgraded son"); 
        }

        if (_Item != null && eventData.button == PointerEventData.InputButton.Left)
        {

        }
    }
}
