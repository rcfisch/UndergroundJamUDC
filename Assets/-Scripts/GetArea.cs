using UnityEngine;
using System;

public class GetArea : MonoBehaviour{
    public bool isInfinite;
    public long item;
    
    public long GetItem()
    {
        if (!isInfinite){
            Destroy(gameObject);
        }
        return item;
    }
}