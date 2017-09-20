using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotEventSystem : MonoBehaviour,  IPointerEnterHandler , IPointerExitHandler{

    public ItemsEventSystem slotItem;
    public int stackcounter;
    public Text StackCounterText;

    private void Start()
    {
        Text counterdisplay = StackCounterText.GetComponent<Text> ();
        counterdisplay.text = stackcounter.ToString ();
        StackCounterText.enabled = false;
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
            Inventory.instance.slotUnderPointer = this;
    }

    public void OnPointerExit (PointerEventData eventData)
    {
    }

    public void StackItem (int stackcount)
    { 
        stackcounter += stackcount;
        StackCounterText.text = stackcounter.ToString ();
    }
}
