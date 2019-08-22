using System;
using UnityEngine;

[CreateAssetMenu]
public class PointProperty : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector3 InitialValue;

    [NonSerialized]
    public Vector3 Value;

    public void OnBeforeSerialize() { }
    
    public void OnAfterDeserialize()
    {
        Value = InitialValue;
    }
}
