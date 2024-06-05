using UnityEngine;
using System;

public class GetArea : MonoBehaviour{
    public bool isInfinite;
    public long item;
    [HideInInspector] public Action callback = null;
    
    public long GetItem()
    {
        if (!isInfinite){
            Destroy(gameObject);
        }

        if (callback != null){
            callback();
        }
        return item;
    }
}