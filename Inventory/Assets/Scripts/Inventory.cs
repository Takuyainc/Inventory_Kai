using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    GameObject InventoryPanel;
    GameObject SlotPanel;
    ItemDB database;
    public GameObject InventorySlot;
    public GameObject InventoryItem;
    int slotAmount;

    List<ItemDB.Item> InventoryItems = new List<ItemDB.Item>();
    List<GameObject> Slots = new List<GameObject>();
  
    public static Inventory instance;
    public ItemsEventSystem draggingItem;
    public SlotEventSystem slotUnderPointer;

    void Awake() {
        instance = this;
    }

    void Start()
    {

        database = GetComponent<ItemDB>();
        slotAmount = 28;
        InventoryPanel = GameObject.Find("Inventory Panel");
        SlotPanel = GameObject.Find("SlotPanel");

        for (int i = 0; i < slotAmount; i++) {

            InventoryItems.Add(new ItemDB.Item());
            Slots.Add(Instantiate(InventorySlot));
            Slots[i].transform.SetParent(SlotPanel.transform);
        }
    }


    public void AddRandomItem() {


        AddItem(Random.Range(0,ItemDB.database.Count));

    }

    public void AddItem(int BaseID) {

        ItemDB.Item ItemtoAdd = database.GetItemByID(BaseID);
        for (int i = 0; i < InventoryItems.Count; i++) {

            if (InventoryItems[i].ID == -1) {
                InventoryItems[i] = ItemtoAdd;
                GameObject InventoryObject = Instantiate(InventoryItem);
                InventoryObject.transform.SetParent(Slots[i].transform);
                InventoryObject.transform.localPosition = Vector2.zero;
                InventoryObject.name = ItemtoAdd.Title;
                ItemsEventSystem item = InventoryObject.GetComponent<ItemsEventSystem>();
                item.setItem(ItemtoAdd);
                item.databasereference = database;
                print(InventoryItems.Count + "Wurden hinzugefügt");
                break;
            }
        }
    }

}