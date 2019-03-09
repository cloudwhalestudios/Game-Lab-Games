using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class AccessibilityInputManager : MonoBehaviour
{
    #region Singleton Instance
    private static AccessibilityInputManager _instance;
    public static AccessibilityInputManager Instance { get { return _instance; } }
    #endregion

    public enum InputMode
    {
        OneButton,
        TwoButtons,
        GamePad,
        Keyboard,
    }

    public InputMaster controls;

    [Header("Player configuration")]
    public Transform playerParentTransform;
    public GameObject playerPrefab;
    public InputMode globalInputMode = InputMode.TwoButtons;
    [ReadOnly, SerializeField] public List<Player> players;
    private static int NEXT_PLAYER_ID = 0;


    public void Awake()
    {
        #region Singleton Instance
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
        #endregion

        // Hotswap (track device changes)
        InputSystem.onDeviceChange +=
            (device, change) =>
            {
                if (change == InputDeviceChange.Added)
                    print($"{device.displayName}: New Device");
                else if (change == InputDeviceChange.Disconnected)
                    print($"{device.displayName}: Device got unplugged");
                else if (change == InputDeviceChange.Reconnected)
                    print($"{device.displayName}: Plugged back in");
                else if (change == InputDeviceChange.Removed)
                    /* Remove from input system entirely; by default, devices stay in the system once discovered */
                    print($"{device.displayName}: Remove from input system entirely");
                ;
            };

        // Fetch any device input
        var deviceInputAction = new InputAction(binding: "/*/<button>");//, interactions: "press(triggerBehaviour=TriggerBehaviour.PressAndRelease)");
        deviceInputAction.performed += HandleNewInput;
            
        deviceInputAction.Enable();

        /*controls.TwoButtons.PrimaryAction.performed += ctx => {  };
        controls.TwoButtons.SecondaryAction.performed += ctx => {  };*/
    }

    #region New Input
    public void OnEnable()
    {
        controls.TwoButtons.Enable();
    }
    public void OnDisable()
    {
        controls.TwoButtons.Disable();
    }
    #endregion

    public void HandleNewInput(InputAction.CallbackContext ctx)
    {
        var dID = ctx.control.device.id;
        var name = ctx.control.device.displayName;

        // Check for player with device
        if (!IsRegisteredToPlayer(dID))
        {
            Debug.Log($"Button {ctx.control.displayName} " +
            $"on new device {name} pressed!" +
            $" (Device ID: {dID})");
            // Create a new player
            CreatePlayer(dID, name);
        }
    }

    public bool IsRegisteredToPlayer(int deviceID)
    {
        if (players == null || players.Count == 0) return false;
        return players.Find(player => player.DeviceID == deviceID) != null;
    }

    // Create a new instance instead
    public void CreatePlayer(int deviceID, string deviceName)
    {
        var newPlayerObject = Instantiate(playerPrefab, playerParentTransform);
        var player = newPlayerObject.GetComponent<Player>();

        if (players == null)
        {
            players = new List<Player>();
        }

        if (players.Count == 0)
        {
            player.isPlayerOne = true;
        }

        player.Register(NEXT_PLAYER_ID++, deviceID, deviceName);
        players.Add(player);
    }

    public void DeregisterPlayer(Player _player)
    {
        if (players != null)
        {
            players.Remove(_player);
        }
    }
}
