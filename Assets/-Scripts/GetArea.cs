using UnityEngine;
using System;

public class GetArea : MonoBehaviour{
    [SerializeField] private bool isInfinite;
    [SerializeField] private long item;
    
    public long GetItem()
    {
        if (!isInfinite){
            Destroy(gameObject);
        }
        return item;
    }
}