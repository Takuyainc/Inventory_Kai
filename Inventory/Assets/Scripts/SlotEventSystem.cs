using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotEventSystem : MonoBehaviour,  IPointerEnterHandler , IPointerExitHandler{

    public ItemsEventSystem slotItem;
    public int stackcounter;
    public Text StackCounterText;

    /// <summary>
    /// Referenz auf die Textkomponente um die Anzahl der Stacks anzuzeigen. Standardmäßig auf enabled = false, damit Stacks erst angezeigt werden, wenn es nötig ist
    /// </summary>

    private void Start()
    {
        Text counterdisplay = StackCounterText.GetComponent<Text> ();   
        counterdisplay.text = stackcounter.ToString ();
        StackCounterText.enabled = false;
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
            Inventory.instance.slotUnderPointer = this;     // der Slot unter dem Mauszeiger wird als aktiver Slot betrachtet
    }

    public void OnPointerExit (PointerEventData eventData)
    {
    }

    public void StackItem (int stackcount)  // Funktion um Stacks zu berechnen und anzuzeigen
    { 
        stackcounter += stackcount;
        StackCounterText.text = stackcounter.ToString ();
    }
}
