using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Currency : MonoBehaviour
{
    public Text MoneyAmount;
    public int Money;
    ItemDB db;

    // Use this for initialization
    void Start()
    {
        db = ItemDB.FindObjectOfType<ItemDB>();
        db.playerMoney = Money;
        Text moneydisplay = MoneyAmount.GetComponent<Text>();
        moneydisplay.text = Money.ToString();
    }

    public void Addmoney(int moneytoAdd) {

        moneytoAdd = Money + 50;
        Money = moneytoAdd;
        Debug.Log(moneytoAdd);
        SetText(moneytoAdd.ToString());
    }

    public void SetText(string moneyAmount) {
        MoneyAmount.text = moneyAmount;
    }

}
