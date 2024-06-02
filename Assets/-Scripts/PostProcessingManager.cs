using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingManager : MonoBehaviour{
    private static PostProcessingManager instance;
    [Header("Settings")] 
    [SerializeField] private float maxFog;
    [SerializeField] private float minFog;
    [Header("References")]
    [SerializeField] private Volume deathVolume;
    [SerializeField] private Volume normalVolume;
    
    private float lerpToWeight;
    private float weight;
    private float time;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
    }
    private void Update()
    {
        var weightPerSec = (weight - lerpToWeight) / time;
        weight -= weightPerSec * Time.deltaTime;
        
        weight = Mathf.Clamp(weight, 0, 1);
        deathVolume.weight = weight;
        instance.normalVolume.weight = 1 - weight;

        RenderSettings.fogDensity = (instance.maxFog - instance.minFog) * weight + instance.minFog;
    }

    public static void SetDeathWeight(float weight, float time)
    {
        instance.lerpToWeight = Mathf.Clamp(weight, 0, 1);
        instance.time = time;
    }
}
