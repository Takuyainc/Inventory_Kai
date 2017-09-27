using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;

[System.Serializable]
public class SerializableBase
{
    /// <summary>
    /// Manche Serialisierer rufen das hier auf (JSON.net sollte es tun)
    /// </summary>
    /// <param name="context"></param>
    
    [OnDeserializing]
    void BeforeDeserializing (StreamingContext context)
    {
        _OnConstruct ();    // Serialisierung
    }

    bool wasConstructed = false;

    void _OnConstruct()     // Sicherheitsmaßnahme um zu checken ob serialisiert wurde und im Falle das nicht, dies noch geschieht
    {
        if (wasConstructed) return;
        wasConstructed = true;

        OnConstruct();
    }

    /// <summary>
    /// Wird bei Erstellung oder Deserialisierung aufgerufen. Manche serialisierer rufen es nicht auf!
    /// </summary>
    
    public SerializableBase ()
    {
        _OnConstruct ();
    }

    /// <summary>
    /// Wird bei onConstruct ODER Deserialisierung augerufen. Abhängig davon was zuerst aufgerufen wird
    /// </summary>
    
    protected virtual void OnConstruct ()
    {
    }
}
