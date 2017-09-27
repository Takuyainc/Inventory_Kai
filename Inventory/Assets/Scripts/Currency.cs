using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Currency : MonoBehaviour
{
    public Text MoneyAmount;
    public int Money;
    ItemDB db;

    void Start () {
        db = ItemDB.FindObjectOfType<ItemDB> ();    // Referenz auf mein Item Objekt
        db.playerMoney = Money;
        Text moneydisplay = MoneyAmount.GetComponent<Text> ();  // Referenz auf Textkomponente in Text
        moneydisplay.text = Money.ToString ();  // ToString um die Menge an Geld optisch darstellen zu können
    }

    public void UpdateAmountText () {

        MoneyAmount.text = "" + db.playerMoney + "$";   // Funktion um das Geld nach jeder Änderung zu updaten
    }

    public void SetText (string moneyAmount) {
        MoneyAmount.text = moneyAmount;
    }

}
