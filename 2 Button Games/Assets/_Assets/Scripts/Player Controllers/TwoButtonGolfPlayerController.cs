using System;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.Experimental.Input.Plugins.PlayerInput;

public class TwoButtonGolfPlayerController : BaseGolfPlayerController
{
    [Header("_______________", order = 0)]
    [Header("Two Button Golf Player Controller", order = 1)]
    [Header("--------------------", order = 2)]

    [Header("Path", order = 3)]
    public Transform selectedPathParent;
    [Range(0, 1f)]
    public float pointInterpolation = 0.2f;

    [Header("Angle")]
    [Range(0, 360f)] public float minAngle;
    [Range(0, 360f)] public float maxAngle;
    // How precise the angle should get the closer the palyer is to the goal
    [Range(0, 360f)] public float angleStep = 5f;

    [Header("Distance")]
    public float minDistance = 1f;
    public float maxDistance = 500f;
    public int distanceFields = 4;


    [Header("Assistance")]
    public float distanceAnglePrecision;

    public override void Awake()
    {
        base.Awake();

        AccessibilityInputManager.Instance.controls
            .TwoButtons.PrimaryAction.performed += OnPrimaryAction;
        AccessibilityInputManager.Instance.controls
            .TwoButtons.SecondaryAction.performed += OnSecondaryAction;


    }

    public void OnPrimaryAction(InputAction.CallbackContext context)
    {
        if (context.control.device.id != owner.DeviceID) return;
        
    }

    public void OnSecondaryAction(InputAction.CallbackContext context)
    {
        if (context.control.device.id != owner.DeviceID) return;
        
    }

    public void SelectPath()
    {
        // Implement path selection
    }
    public void SelectAngle()
    {
        // Implement angle bounce
    }
    public void SelectDistance()
    {
        // Implement distance fields
    }
}
