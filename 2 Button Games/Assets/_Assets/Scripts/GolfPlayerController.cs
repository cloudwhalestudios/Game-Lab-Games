using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolfPlayerController : MonoBehaviour
{
    public enum State
    {
        None,
        Choose_Direction,
        Select_Power,
        Fire,
        Waiting_For_Other_Players
    }

    [Header("Input Mapping")]
    public KeyCode keyConfirm;
    public KeyCode keyAbort;

    [Header("Behaviour")]
    public float minPowerPercentile = 0.05f;
    public float maxPowerPercentile = 1f;
    public float fillTime = 1f;

    public Slider powerBar;
    public Color minPowerColor = Color.green;
    public Color maxPowerColor = Color.red;

    public float yawStep = 10f;

    [Header("Physics")]
    public float forceMultiplier = 5f;
    public ForceMode forceMode = ForceMode.Impulse;

    [Header("Info")]
    [ReadOnly]
    public State currentState = State.Waiting_For_Other_Players;
    [ReadOnly]
    public Rigidbody rb;
    [ReadOnly]
    public bool filling = true;
    [ReadOnly]
    public float elapsedTime = 0;
    [ReadOnly]
    public float yaw = 0;


    void Start()
    {
        if (powerBar != null)
        {
            powerBar.minValue = minPowerPercentile;
            powerBar.maxValue = maxPowerPercentile;
            powerBar.value = minPowerPercentile;
            UpdateBarColor();
        }

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Choose_Direction: // Player hits confirm button to select direction

                CheckForInput(State.Select_Power);
                break;

            case State.Select_Power: // Power bar starts moving and player stops it
                if (elapsedTime >= fillTime)
                {
                    filling = false;
                    elapsedTime = fillTime;
                }
                else if (elapsedTime <= 0)
                {
                    filling = true;
                    elapsedTime = 0;
                }
                powerBar.value = Mathf.Lerp(minPowerPercentile, maxPowerPercentile, elapsedTime / fillTime);
                elapsedTime = elapsedTime + ((filling) ? +Time.deltaTime : -Time.deltaTime);

                CheckForInput(State.Fire, State.Choose_Direction);
                break;

            case State.Fire: // Force gets applied to the ball
                rb.AddForce(Vector3.forward * powerBar.value * forceMultiplier, forceMode);
                SwitchState(State.Waiting_For_Other_Players);
                break;

            case State.Waiting_For_Other_Players: // Check if it's this players turn

            default:
                break;
        }
        UpdateBarColor();
    }

    void UpdateBarColor()
    {
        float v = powerBar.value - minPowerPercentile / maxPowerPercentile;
        Color col = Color.Lerp(minPowerColor, maxPowerColor, v);
        powerBar.fillRect.GetComponent<Image>().color = col;
    }

    void SwitchState(State newState)
    {
        currentState = newState;
    }

    bool CheckForInput(State confirmState, State abortState = State.None)
    {
        // Check for Confirm or abort
        if (Input.GetKeyDown(keyConfirm) && confirmState != State.None)
        {
            SwitchState(confirmState);
            return true;
        }
        else if (Input.GetKeyDown(keyAbort) && abortState != State.None)
        {
            SwitchState(abortState);
            return true;
        }

        return false;
    }
}
