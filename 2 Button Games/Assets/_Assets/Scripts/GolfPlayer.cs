using System;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.Experimental.Input.Plugins.PlayerInput;

public class GolfPlayer : MonoBehaviour//, ITwoButtonsActions
{
    // Public variable for action map input for this player.
    public InputMaster controls;

    public float forceMultiplier = 100f;
    public ForceMode mode = ForceMode.Force;

    private Rigidbody rb;

    [ReadOnly] public static int id;

    public void Awake()
    {
        //controls = (GolfControls) playerInput.actions.;
        //controls.TwoButtons.SetCallbacks(this);

        rb = GetComponent<Rigidbody>();

        id = GameManager.Instance.playStateController.AddPlayer();

        controls.TwoButtons.PrimaryAction.performed += PrimaryAction;
        controls.TwoButtons.SecondaryAction.performed += SecondaryAction;
    }
    void PrimaryAction(InputAction.CallbackContext c)
    {
        //print("Do the thing! " + c.control.device.displayName + ">" + c.control.displayName);
        rb.AddForce((Vector3.up / 2 + Vector3.forward) * forceMultiplier, mode);
    }

    void SecondaryAction(InputAction.CallbackContext c)
    {
        //print("Stop! " + c.control.device.displayName + ">" + c.control.displayName);
        rb.AddForce((Vector3.up / 2 -Vector3.forward) * forceMultiplier, mode);

    }

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 10, Color.magenta, Time.deltaTime);
    }

    public void OnEnable()
    {
        controls.TwoButtons.Enable();
    }

    public void OnDisable()
    {
        controls.TwoButtons.Disable();
    }
}
