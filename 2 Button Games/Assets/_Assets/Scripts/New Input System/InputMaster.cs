// GENERATED AUTOMATICALLY FROM 'Assets/_Assets/Scripts/New Input System/InputMaster.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


[Serializable]
public class InputMaster : InputActionAssetReference
{
    public InputMaster()
    {
    }
    public InputMaster(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // TwoButtons
        m_TwoButtons = asset.GetActionMap("TwoButtons");
        m_TwoButtons_PrimaryAction = m_TwoButtons.GetAction("PrimaryAction");
        m_TwoButtons_SecondaryAction = m_TwoButtons.GetAction("SecondaryAction");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_TwoButtons = null;
        m_TwoButtons_PrimaryAction = null;
        m_TwoButtons_SecondaryAction = null;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
    }
    // TwoButtons
    private InputActionMap m_TwoButtons;
    private InputAction m_TwoButtons_PrimaryAction;
    private InputAction m_TwoButtons_SecondaryAction;
    public struct TwoButtonsActions
    {
        private InputMaster m_Wrapper;
        public TwoButtonsActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryAction { get { return m_Wrapper.m_TwoButtons_PrimaryAction; } }
        public InputAction @SecondaryAction { get { return m_Wrapper.m_TwoButtons_SecondaryAction; } }
        public InputActionMap Get() { return m_Wrapper.m_TwoButtons; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(TwoButtonsActions set) { return set.Get(); }
    }
    public TwoButtonsActions @TwoButtons
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new TwoButtonsActions(this);
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get

        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.GetControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_MouseSchemeIndex = -1;
    public InputControlScheme MouseScheme
    {
        get

        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.GetControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
        }
    }
    private int m_XBoxOneGamepadSchemeIndex = -1;
    public InputControlScheme XBoxOneGamepadScheme
    {
        get

        {
            if (m_XBoxOneGamepadSchemeIndex == -1) m_XBoxOneGamepadSchemeIndex = asset.GetControlSchemeIndex("XBoxOneGamepad");
            return asset.controlSchemes[m_XBoxOneGamepadSchemeIndex];
        }
    }
    private int m_XInputControllerSchemeIndex = -1;
    public InputControlScheme XInputControllerScheme
    {
        get

        {
            if (m_XInputControllerSchemeIndex == -1) m_XInputControllerSchemeIndex = asset.GetControlSchemeIndex("XInputController");
            return asset.controlSchemes[m_XInputControllerSchemeIndex];
        }
    }
    private int m_CustomHIDTwoButtonSwitchSchemeIndex = -1;
    public InputControlScheme CustomHIDTwoButtonSwitchScheme
    {
        get

        {
            if (m_CustomHIDTwoButtonSwitchSchemeIndex == -1) m_CustomHIDTwoButtonSwitchSchemeIndex = asset.GetControlSchemeIndex("CustomHID (TwoButtonSwitch)");
            return asset.controlSchemes[m_CustomHIDTwoButtonSwitchSchemeIndex];
        }
    }
}
