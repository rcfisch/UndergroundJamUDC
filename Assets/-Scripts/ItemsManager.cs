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
            leftItem = Instantiate(itemPrefabs[item], leftHandPoint.position, quaternion.identity);
        }
        else{
            rightItemID = item;
            rightItem = Instantiate(itemPrefabs[item], rightHandPoint.position, quaternion.identity);
        }
    }
    
    private bool CheckForPlaceArea(long item)
    {
        // ReSharper disable once Unity.PerformanceCriticalCodeCameraMain
        var cam = Camera.main!.gameObject.transform;
        RaycastHit hit;
        Physics.Raycast(cam.position, cam.eulerAngles, out hit, findDistance, ignoreLayers);

        if (hit.transform){
            if (hit.transform.gameObject.GetComponent<PlaceArea>()){
                return hit.transform.gameObject.GetComponent<PlaceArea>().AcceptItem(item);
            }
        }
        return false;
    }
    private long CheckForItem()
    {
        // ReSharper disable once Unity.PerformanceCriticalCodeCameraMain
        var cam = Camera.main!.gameObject.transform;
        RaycastHit hit;
        Physics.Raycast(cam.position, cam.eulerAngles, out hit, findDistance, ignoreLayers);
        if (hit.transform){
            if (hit.transform.gameObject.GetComponent<PlaceArea>()){
                return hit.transform.gameObject.GetComponent<GetArea>().GetItem();
            }
        }

        return -1;
    }    
}