using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;

[System.Serializable]
public class SerializableBase
{
    /// <summary>
    /// Might be executed by some serializers (JSON.net should call this...)
    /// </summary>
    /// <param name="context"></param>
    [OnDeserializing]
    void BeforeDeserializing(StreamingContext context)
    {
        _OnConstruct();
    }

    bool wasConstructed = false;

    void _OnConstruct()
    {
        if (wasConstructed) return;
        wasConstructed = true;

        OnConstruct();
    }

    /// <summary>
    /// Executed on creation or deserialization. Some serializers will NOT call this!
    /// </summary>
    public SerializableBase()
    {
        _OnConstruct();
    }

    /// <summary>
    /// Will be executed on construct OR on deserialization. Whatever is executed first
    /// </summary>
    protected virtual void OnConstruct()
    {

    }
}
