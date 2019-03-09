using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player : MonoBehaviour
{
    /// <summary>
    /// Controller ID (gotten from the game manager)
    /// </summary>
    [ReadOnly, SerializeField] protected int pID = -1;
    [ReadOnly, SerializeField] protected string deviceName = "N/A";
    [ReadOnly, SerializeField] protected int deviceID = -1;

    public MenuController menuController;
    public BaseGolfPlayerController golfController;

    public bool isPlayerOne = false;

    public int PID { get => pID; }
    public int DeviceID { get => deviceID; }
    public string DeviceName { get => deviceName; }

    public virtual void Awake()
    {
        //pID = GameManager.Instance.RegisterPlayer(this);
    }

    public virtual void OnDestroy()
    {
        AccessibilityInputManager.Instance.DeregisterPlayer(this);
    }

    public void Register(int pID, int deviceID, string deviceName)
    {
        this.pID = pID;
        this.deviceID = deviceID;
        this.deviceName = deviceName;
    }
}
