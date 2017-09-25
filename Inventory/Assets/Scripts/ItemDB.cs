using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

public class ItemDB : MonoBehaviour {

    public  static List<Item> database;
    public int playerMoney;
    public Currency currency;
    public SlotEventSystem slotsystem;

    void Awake ()            // Awake weil Datenbank mit dem ersten Frame geladen werden soll
    {
        string path = Application.streamingAssetsPath + "/Items.json";      // Application Funktion um den StreamingAssets Ordner anzugeben und den Namen der .json
        string jsonstring = File.ReadAllText (path);                         // String jsonstring enthält den Inhalt von Items.json
        database = JsonConvert.DeserializeObject<List<Item>> (jsonstring);   // Befüllen der Liste database mit allen Einträgen aus der JSON
    }

    public void Addmoney (int money)    
    { 
        playerMoney += money;   // Das Geld des Spielers wird erhöht sobald Addmoney aufgerufen wird
    }

    /// <summary>
    /// Get the item from the database that has the ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// 

    public Item GetItemByID (int ID)     // Beispielhafte Methode zum finden eines Items in meiner Datenbank anhand seiner ID
    {
        return database.Find (item => item.ID == ID);
    }

    public Item GetbyBaseID (string BaseID, Item.TierEnum tier)      // Methode zum finden eines Items in meiner Datenbank anhand seiner BaseID und Tier
    {
        return database.Find (item => item.ItemBaseID == BaseID && item.Tier == tier);
    }

    public int UpgradeMoney (Item selectedItem)     // Funktion um das Geld des Spielers nach einem Upgrade anzupassen
    {
        playerMoney = playerMoney - selectedItem.Value;
        currency.UpdateAmountText ();
        return playerMoney;
    }

    public Item Upgradeitem (Item itemToUpgrade)    // Aktuelles Item nutzen um eine Abfrage zu starten
    {
       
        if(playerMoney < itemToUpgrade.Value) { // Aktuelles Geld nutzen um zu checken ob man sich ein upgrade leisten kann

            print ("You can not upgrade further");
            return itemToUpgrade;
        }

        if (playerMoney >= itemToUpgrade.Value) { 
        
            if (itemToUpgrade.Tier == Item.TierEnum.epic) {    // Abgleichen ob ein größeres Tier vorhanden ist, ansonsten macht ein Upgrade keinen Sinn

                print("Highest Tier reached");
                return itemToUpgrade;
                }
                                                                          
                UpgradeMoney (itemToUpgrade);                                // Kosten für das Upgrade vom Geld abziehen
               
                return GetbyBaseID (itemToUpgrade.ItemBaseID, itemToUpgrade.Tier+1);  // Rückgabe des Items in der DB, welches durch aktuelles Item ersetzt wird
            }
        return itemToUpgrade;       // Falls kein Fall eintritt, ursprüngliches Item wieder zurückgeben
    }
    

     [System.Serializable]
    public class Item : SerializableBase {

        protected override void OnConstruct()
        {
            base.OnConstruct();

            this.ID = -1;       // Ich setze hier die ID des Objektes Item auf -1, da ich in einer früheren Version damit eine Sicherheitsmaßnahme getroffen habe um keine leeren
        }                       // Items zu spawnen. Ist jetzt nichtmehr notwendig, kann aber auch nicht schaden.
        

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

        public Item (string baseID, int id, string title, int value, int defense, int damage, int magicresistance, int capacity, int armor, bool stackable, string description, 
                        int rarity, string slug, int upgradeCounter, TierEnum tier) : base () { 
        
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
            this.itemsprite = Resources.Load<Sprite> ("Items/" + slug);
        }

        public Item () : base ()        // Leerer Konstruktor für Item
        {  
        }
    }
}
