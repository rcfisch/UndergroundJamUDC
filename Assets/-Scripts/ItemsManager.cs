using System;
using Unity.Mathematics;
using UnityEngine;

public class ItemsManager : MonoBehaviour{
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private GameObject[] itemDroppedPrefabs;
    [Space(8)]
    [SerializeField] private Transform leftHandPoint;
    [SerializeField] private Transform rightHandPoint;
    [Space(8)] 
    [SerializeField] private float findDistance;
    [SerializeField] private LayerMask ignoreLayers;
    
    private long leftItemID = -1;
    private long rightItemID = -1;

    private GameObject leftItem;
    private GameObject rightItem;

    
    private void Update()
    {
        var lmd = Input.GetMouseButtonDown(0);
        var rmd = Input.GetMouseButtonDown(1);

        if (lmd){
            if (leftItemID != -1){
                if (CheckForPlaceArea(leftItemID)){
                    leftItemID = -1;
                    Destroy(leftItem);
                }
            }
            else{
                var item = CheckForItem();
                if (item != -1){
                    SetItemToHand(item, true);
                    leftItemID = item;
                }
            }
        }

        if (rmd){
            if (rightItemID != -1){
                if (CheckForPlaceArea(rightItemID)){
                    rightItemID = -1;
                    Destroy(rightItem);
                }
            }
            else{
                var item = CheckForItem();
                if (item != -1){
                    SetItemToHand(item, false);
                    rightItemID = item;
                }
            }
        }
    }

    private void SetItemToHand(long item, bool leftHand)
    {
        if (leftHand){
            leftItemID = item;
            leftItem = Instantiate(itemPrefabs[item], leftHandPoint);
        }
        else{
            rightItemID = item;
            rightItem = Instantiate(itemPrefabs[item], rightHandPoint);
        }
    }
    
    private bool CheckForPlaceArea(long item)
    {
        var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2,Screen.height/2));
        
        Physics.Raycast(ray, out var hit, findDistance);
        Debug.DrawRay(ray.origin, ray.direction, Color.blue, 10, true);
        if (hit.transform){
            if (hit.transform.gameObject){
                var pa = hit.transform.gameObject.GetComponent<PlaceArea>();
                if (pa){
                    return pa.AcceptItem(item);
                }
                else{
                    Debug.Log("No Get Area");
                }
            }
            else{
                Debug.Log("GameObject is null");
            }
        }
        else{
            Debug.Log("Transform is null");
        }

        return false;
    }
    private long CheckForItem()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2,Screen.height/2));
        
        Physics.Raycast(ray, out var hit, findDistance);
        Debug.DrawRay(ray.origin, ray.direction, Color.blue, 10, true);
        if (hit.transform){
            if (hit.transform.gameObject){
                var item = hit.transform.gameObject.GetComponent<GetArea>();
                if (item){
                    return item.GetItem();
                }
                else{
                    Debug.Log("No Get Area");
                }
            }
            else{
                Debug.Log("GameObject is null");
            }
        }
        else{
            Debug.Log("Transform is null");
        }
        return -1;
    }    
}