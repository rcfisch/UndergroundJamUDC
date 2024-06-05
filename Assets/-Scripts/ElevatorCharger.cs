using UnityEngine;

public class ElevatorCharger : MonoBehaviour{
    [HideInInspector] public bool charged = false;
    
    private GetArea ga;
    private PlaceArea pa;
    [SerializeField] private GameObject battery;
    
    [SerializeField] private GameObject batteryChargedModel;

    private void Awake()
    {
        pa = GetComponent<PlaceArea>();
        pa.enabled = false;
        pa.callback = OnBatteryGiven;

        ga = GetComponent<GetArea>();
        ga.callback = OnBatteryTaken;
    }

    public void OnBatteryTaken()
    {
        Destroy(battery);
        pa.enabled = true;
    }
    public void OnBatteryGiven(long item)
    {
        Instantiate(batteryChargedModel, transform);
        charged = true;
        pa.enabled = false;
    }
}