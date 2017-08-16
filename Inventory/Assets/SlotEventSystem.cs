using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotEventSystem : MonoBehaviour,  IPointerEnterHandler , IPointerExitHandler{

    public ItemsEventSystem slotItem;

    public void OnPointerEnter(PointerEventData eventData)
    {
        
            Inventory.instance.slotUnderPointer = this;
        
        
    }

    public void OnPointerExit(PointerEventData eventData) {

 //       Inventory.instance.slotUnderPointer = null;

    }
}
