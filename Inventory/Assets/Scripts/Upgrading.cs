using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class Upgrading
{
    public int UpgradeCost;
    public List<int> valueSteps = new List<int>();

    public Upgrading()
    {
        valueSteps.Add(UpgradeCost = 5);
        valueSteps.Add(UpgradeCost = 10);
        valueSteps.Add(UpgradeCost = 20);
        valueSteps.Add(UpgradeCost = 50);
        valueSteps.Add(UpgradeCost = 100);
    }
}