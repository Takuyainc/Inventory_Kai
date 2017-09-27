using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    GameObject inventoryPanel;      //Gameobject, welches mein Inventar hält
    GameObject slotPanel;           //Gameobject, welches meine Slots hält
    ItemDB database;
    public GameObject inventorySlot;        //Public um den Slot als Prefab im Inspector zu setzen
    public GameObject inventoryItem;        //Public um die Items als Prefab im Inspector zu setzen (Wird über eine Identifizierung dynamisch selbst gesetzt)
    int slotAmount;

    List<ItemDB.Item> inventoryItems = new List<ItemDB.Item>();
    List<SlotEventSystem> slots = new List<SlotEventSystem>();
  
    public static Inventory instance;
    public ItemsEventSystem draggingItem;
    public SlotEventSystem slotUnderPointer;
    public GameObject dragPanel;

    void Awake()
    {
        instance = this;    // Nötig um Inventory Global einfach zugänglich zu machen
    }

    /// <summary>
    /// On Start wird das Inventar erstellt. Sollte man SlotAmount Public stellen, könnte man im Inspector die Slotanzahl dynamisch setzen. Wegen der gewählten Größe des Inventar auf 28.
    /// Slots bekommen sofort ihren entsprechenden Parent um spätere onClick und onDrag zu ermöglichen.
    /// Jeder Slot bekommt eine Nummer zugewiesen um im Inspector einfacher identifiziert werden zu können
    /// </summary>
     
    void Start()
    {
        dragPanel = GameObject.Find("DragPanel");
        database = GetComponent<ItemDB>();
        slotAmount = 28;
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = GameObject.Find("SlotPanel");

        for (int i = 0; i < slotAmount; i++) {

            slots.Add (Instantiate (inventorySlot).GetComponent<SlotEventSystem> ());
            slots[i].transform.SetParent(slotPanel.transform);
            slots[i].name = "" + i;
        }
    }


    public void AddRandomItem()
    {
        AddItem (Random.Range (0,ItemDB.database.Count));   // Nur nötig, damit Testweise alle möglichen Items gespawnt werden, wenn man es möchte. Hat keinen Sinn für echte Inventar Anwendungen.
    }

    /// <summary>
    /// Das Richtige Item, dass gespawned werden soll, wird anhand seiner ID gecheckt.
    /// Es wird über jeden vorhandenen Slot gelaufen, ob dieser ein Item enthält dessen stackable = true ist, der Slot nicht leer ist, die ID mit der ID des Item das zu spanwen ist übereinstimmt und ob dessen Tier identisch ist.
    /// Falls das so ist, wird ein Stack auf dem Slot erstellt.
    /// Falls der Slot leer ist, wird ein neues Item im nächsten verfügbaren gespawned.
    /// Position und Parent des Items wird dynamisch auf die Position des Slots zugewiesen, damit es mittig spawned.
    /// </summary>
    /// <param name="ID"></param>
    
    public void AddItem (int ID)
    {
        ItemDB.Item ItemtoAdd = database.GetItemByID(ID);
        for (int i = 0; i < slots.Count; i++) {

            if (ItemtoAdd.Stackable && slots[i].slotItem != null && slots[i].slotItem._item.ID == ItemtoAdd.ID && slots[i].slotItem._item.Tier == ItemtoAdd.Tier) { 

                        slots[i].StackItem (1);
                        print(slots[i].slotItem._item.Armor);
                break;

            } else if (slots[i].slotItem == null) {
                inventoryItems.Add(ItemtoAdd);
                GameObject InventoryObject = Instantiate(inventoryItem);
                InventoryObject.transform.SetParent(slots[i].transform);
                InventoryObject.transform.localPosition = Vector2.zero;
                InventoryObject.name = ItemtoAdd.Title;
                ItemsEventSystem item = InventoryObject.GetComponent<ItemsEventSystem>();
                item.slot = slots[i];
                slots[i].slotItem = item;
                item.setItem(ItemtoAdd);
                item.databasereference = database;

                print(inventoryItems.Count + " Wurden hinzugefügt");
                print(slots[i].slotItem._item.Stackable + " ist nicht stackable");
                break;
            }
        }
    }

    public void AddItem (ItemDB.Item ItemtoAdd)
    {
        for (int i = 0; i < slots.Count; i++) {

            if (ItemtoAdd.Stackable && slots[i].slotItem != null && slots[i].slotItem._item.ID == ItemtoAdd.ID && slots[i].slotItem._item.Tier == ItemtoAdd.Tier) { 

                slots[i].StackItem (1);
                print (slots[i].slotItem._item.Armor);
                break;

            } else if (slots[i].slotItem == null) {

                inventoryItems.Add (ItemtoAdd);
                GameObject InventoryObject = Instantiate (inventoryItem);
                InventoryObject.transform.SetParent (slots[i].transform);
                InventoryObject.transform.localPosition = Vector2.zero;
                InventoryObject.name = ItemtoAdd.Title;
                ItemsEventSystem item = InventoryObject.GetComponent<ItemsEventSystem> ();
                item.slot = slots[i];
                slots[i].slotItem = item;
                item.setItem (ItemtoAdd);
                item.databasereference = database;

                print (inventoryItems.Count + " Wurden hinzugefügt");             
                print (slots[i].slotItem._item.Stackable + " ist nicht stackable");
                break;
            }
        }
    }

}