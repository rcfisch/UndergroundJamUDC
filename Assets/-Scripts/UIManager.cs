using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour{
    [HideInInspector] public static PlayerMovement pm;
    public Slider staminaBar;

    private void Update()
    {
        staminaBar.value = pm.sprintCounter / pm.sprintTime;
    }
}
