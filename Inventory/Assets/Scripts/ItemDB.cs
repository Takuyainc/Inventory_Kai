using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

public class ItemDB : MonoBehaviour
{
    /// <summary>
    /// Contains all items from the item database
    /// </summary>
    private static List<Item> database;
    public int playerMoney;
    Currency currency = new Currency();

    void Awake()            // Awake weil Datenbank mit dem ersten Frame geladen werden soll
    {
        string path = Application.streamingAssetsPath + "/Items.json";
        string jsonstring = File.ReadAllText(path);
        database = JsonConvert.DeserializeObject<List<Item>>(jsonstring);       // Befüllen der Liste database mit allen Einträgen aus der JSON
    }

    private void Update()
    {
        playerMoney = currency.Money;
    }

    /// <summary>
    /// Get the item from the database that has the ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public Item GetItemByID(int ID)     //Beispielhafte Methode zum finden eines Items in meiner Datenbank anhand seiner ID
    {
        return database.Find(item => item.ID == ID);
    }

    public Item GetbyBaseID(string BaseID, Item.TierEnum tier)      //Methode zum finden eines Items in meiner Datenbank anhand seiner BaseID und Tier
    {
        return database.Find(item => item.ItemBaseID == BaseID && item.Tier == tier);
    }

    public int UpgradeMoney(int newMoney, Item selectedItem)
    {
        newMoney = playerMoney - selectedItem.Value;
        return playerMoney = newMoney;
    }

    public Item Upgradeitem(Item itemToUpgrade, int money) //aktuelles Item nutzen um eine Abfrage zu starten
    {
        money = playerMoney;            // aktuelles Geld nutzen um zu checken ob man sich ein upgrade leisten kann

        if(money < itemToUpgrade.Value){
            throw new System.Exception();
        }

        if (money >= itemToUpgrade.Value)
        {
                Item.TierEnum upgradeTier = Item.TierEnum.epic;  //abgleichen ob ein größeres Tier vorhanden ist

                if (itemToUpgrade.Tier != Item.TierEnum.epic)   // wenn nicht epic, dann:
                {
                    if (itemToUpgrade.Tier != Item.TierEnum.rare) // wenn nicht rare, dann mach rare
                    {
                        upgradeTier = Item.TierEnum.rare;
                    }
                    else                                          //wenn nicht rare && normal, dann epic
                    {
                        upgradeTier = Item.TierEnum.epic;

                    }
                }
                                                                            //altes item im spielerinventar mit neuem tauschen
                UpgradeMoney(money, itemToUpgrade);                         //Kosten für das Upgrade vom Geld abziehen
                return GetbyBaseID(itemToUpgrade.ItemBaseID, upgradeTier);  //Rückgabe des Items in der DB, welches durch aktuelles Item ersetzt wird
            }
        return itemToUpgrade;
    }
    


    [System.Serializable]
    public class Item : SerializableBase
    {
        protected override void OnConstruct()
        {
            base.OnConstruct();
            Debug.Log("works boy");

            this.ID = -1;
        }

        public enum TierEnum {              // Enumeration für die Tiers (Name Tierenum)
            normal,
            rare,
            epic
        }

        public string ItemBaseID { get; set; }
        public int ID { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
        public int Defense { get; set; }
        public int Damage { get; set; }
        public int MagicResistance { get; set; }
        public int Capacity { get; set; }
        public int Armor { get; set; }
        public bool Stackable { get; set; }
        public string Description { get; set; }
        public int Rarity { get; set; }
        public string Slug { get; set; }
        public int UpgradeCounter { get; set; }                  // Eigentliche deklaration meines Tier Objektes mit Tierenum als Typ
        public Sprite itemsprite { get; set; }
        public TierEnum Tier;

        public Item(string baseID, int id, string title, int value, int defense, int damage, int magicresistance, int capacity, int armor, bool stackable, string description, int rarity, string slug, int upgradeCounter, TierEnum tier) : base()
        {
            this.ItemBaseID = baseID;
            this.ID = id;
            this.Title = title;
            this.Value = value;
            this.Defense = defense;
            this.Damage = damage;
            this.MagicResistance = magicresistance;
            this.Capacity = capacity;
            this.Armor = armor;
            this.Stackable = stackable;
            this.Description = description;
            this.Rarity = rarity;
            this.Slug = slug;
            this.UpgradeCounter = upgradeCounter;
            this.Tier = tier;
            this.itemsprite = Resources.Load<Sprite>("items/" + slug);
        }

        public Item() : base()
        {
        }
    }
}
