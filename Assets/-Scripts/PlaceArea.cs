using UnityEngine;
using System;

public class PlaceArea : MonoBehaviour{
    [SerializeField] private long[] acceptedItems;
    [HideInInspector] public Action<long> callback = null;
    
    public bool AcceptItem(long tryItem)
    {
        foreach (var item in acceptedItems){
            if (tryItem == item){
                if (callback != null){
                    callback(tryItem);   
                }
                else{
                    Debug.LogWarning("Callback is null, please assign a callback script to this object.");
                }
                return true;
            }
        }
        return false;
    }
}