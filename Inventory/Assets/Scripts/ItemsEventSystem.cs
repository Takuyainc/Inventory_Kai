using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemsEventSystem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{

    Sprite itemicon;
    public ItemDB.Item _item;
    public ItemDB databasereference;

    public SlotEventSystem slot;

    Vector3 initialPosition;

    public void setItem (ItemDB.Item newItem)       // Funktion um das richtige Item "auszuwählen". Das Item bekommt sein entsprechendes Sprite zugewiesen.
    {
        _item = newItem;
        GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Items/" + _item.Slug);

        if (_item.Stackable) {

            slot.StackCounterText.enabled = true;   // Falls ein Stack vorhanden ist, wird der Counter aktivitert.
            slot.StackItem(1);

        } else {

            slot.StackCounterText.enabled = false;
        }
    }

    /// <summary>
    /// Falls der Slot ein Item enthält, wird ein Drag möglich sein. Dafür wird das Item neu geparentet, damit die Raycasts treffen können (Das Dragpanel liegt in der Hierarchie
    /// über den Slots, somit wird dieser Raycast nie verdeckt.)
    /// </summary>
    /// <param name="eventData"></param>
     
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_item != null) {
            GetComponent<Image> ().raycastTarget = false;
            initialPosition = transform.position;
            Inventory.instance.draggingItem = this;
            this.transform.position = eventData.position;
            this.transform.SetParent (Inventory.instance.dragPanel.transform);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_item != null) {
            this.transform.position = eventData.position;
        }
    }

    public void OnEndDrag (PointerEventData eventData)
    {
        GetComponent<Image> ().raycastTarget = true;
        Inventory.instance.draggingItem = null;
        if (Inventory.instance.slotUnderPointer.slotItem == null) {  // Inventory instance ist eine referenz. 
            int oldstackcounter = slot.stackcounter;    // Stackcount wird temporär gespeichert
            slot.slotItem = null;
            slot.stackcounter = 0;
            slot.StackCounterText.enabled = false;

            slot = Inventory.instance.slotUnderPointer;     // Der Slot wird verändert.
            slot.StackCounterText.enabled = true;
            slot.StackItem (oldstackcounter);   // StackItem wird im Falle das das item stackable ist aufgerufen und bekommt den Wert des temporär gespeicherten Counters

            slot.slotItem = this;    // Ich sage dem slot auf dem ich lande, ich bin dein neues Item.
            transform.position = slot.transform.position;   // Transform der Position.
            transform.SetParent (slot.transform);    // Neu parenten damit der raycast ausgeführt werden kann. Andernfalls ist kein Drag mehr möglich.
        } else { 
            transform.position = initialPosition;   // Falls das Item keinen neuen Slot trifft, snapped es wieder zurück zum alten Slot.
            this.transform.SetParent(slot.transform);   // Erneut parenten, damit anschließender Drag wieder möglich ist.
        }
    }

    /// <summary>
    /// Ist ein Item vorhanden und hat es einen Stack von 1, wird Upgradeitem aufgerufen. Für den Fall das der Stackcounter größer als 1 ist, was einem Stackable Item entspricht,
    /// wird weiterhin gecheckt ob das entstehende Item ein anderes ist, als das zu upgradende (reine Testzwecke, trifft immer zu).
    /// Nun wird der Stackcount um 1 heruntergesetzt und ein neues Item wird dem Inventar hinzugefügt.
    /// </summary>
    /// <param name="eventData"></param>
    /// 
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_item != null && eventData.button == PointerEventData.InputButton.Right) {  // Rechte Maustaste auf einem Item

            if (slot.stackcounter == 1 || !_item.Stackable) {

                setItem(databasereference.Upgradeitem(_item));      //Debug.Log("_Item:  "+(_Item == null)+" databasereference: "+ (databasereference==null));
                Debug.Log(_item.Title + " has been upgraded son");

            } else if (slot.stackcounter > 1) {

                ItemDB.Item upgradedItem = databasereference.Upgradeitem (_item);
                if (upgradedItem != _item) {

                    slot.StackItem(-1);
                    Inventory.instance.AddItem (databasereference.Upgradeitem (_item));   // Upgraded das Item aus dem Stack und fügt es in einen neuen Slot hinzu
                }
            }

        }

        if (_item != null && eventData.button == PointerEventData.InputButton.Left) {
            
            }
        }
}

