using System;
using System.Collections;
using UnityEngine;

public class Charger : MonoBehaviour{
    [SerializeField] private float chargingTime;
    
    private GetArea ga;
    private PlaceArea pa;
    private GameObject battery;
    
    [SerializeField] private GameObject batteryModel;
    [SerializeField] private GameObject readyLight;
    [SerializeField] private GameObject notReadyLight;

    
    private void Awake()
    {
        pa = GetComponent<PlaceArea>();
        pa.callback = OnBatteryPlaced;
        
        notReadyLight.SetActive(false);
        readyLight.SetActive(false);
    }

    public void OnBatteryPlaced(long item)
    {
        battery = Instantiate(batteryModel, transform);
        Destroy(pa);
        StartCoroutine(Charging());
    }

    private IEnumerator Charging()
    {
        notReadyLight.SetActive(true);
        yield return new WaitForSeconds(chargingTime);
        
        notReadyLight.SetActive(false);
        readyLight.SetActive(true);

        ga = gameObject.AddComponent<GetArea>();
        ga.item = 1;
        ga.isInfinite = false;
    }
}
