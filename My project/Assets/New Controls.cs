// GENERATED AUTOMATICALLY FROM 'Assets/New Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @NewControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @NewControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""New Controls"",
    ""maps"": [
        {
            ""name"": ""Xbox"",
            ""id"": ""92e89ff3-3fbd-46a3-987a-92fbfd16025f"",
            ""actions"": [
                {
                    ""name"": ""X"",
                    ""type"": ""Button"",
                    ""id"": ""6d97a11f-5d75-4550-a695-e5ced675c49f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""A"",
                    ""type"": ""Button"",
                    ""id"": ""91d25d1e-126f-4b46-90bc-0b72c155c275"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""87533f3f-91eb-4d79-bf5e-8fea3891804d"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""XboxScheme"",
                    ""action"": ""X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3afbac09-5979-4f92-99df-d0ce68c1c94b"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxScheme"",
                    ""action"": ""A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eab5e355-de4f-4ce1-99c2-76278d4ca77e"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""XboxScheme"",
                    ""action"": ""A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""XboxScheme"",
            ""bindingGroup"": ""XboxScheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Xbox
        m_Xbox = asset.FindActionMap("Xbox", throwIfNotFound: true);
        m_Xbox_X = m_Xbox.FindAction("X", throwIfNotFound: true);
        m_Xbox_A = m_Xbox.FindAction("A", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Xbox
    private readonly InputActionMap m_Xbox;
    private IXboxActions m_XboxActionsCallbackInterface;
    private readonly InputAction m_Xbox_X;
    private readonly InputAction m_Xbox_A;
    public struct XboxActions
    {
        private @NewControls m_Wrapper;
        public XboxActions(@NewControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @X => m_Wrapper.m_Xbox_X;
        public InputAction @A => m_Wrapper.m_Xbox_A;
        public InputActionMap Get() { return m_Wrapper.m_Xbox; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(XboxActions set) { return set.Get(); }
        public void SetCallbacks(IXboxActions instance)
        {
            if (m_Wrapper.m_XboxActionsCallbackInterface != null)
            {
                @X.started -= m_Wrapper.m_XboxActionsCallbackInterface.OnX;
                @X.performed -= m_Wrapper.m_XboxActionsCallbackInterface.OnX;
                @X.canceled -= m_Wrapper.m_XboxActionsCallbackInterface.OnX;
                @A.started -= m_Wrapper.m_XboxActionsCallbackInterface.OnA;
                @A.performed -= m_Wrapper.m_XboxActionsCallbackInterface.OnA;
                @A.canceled -= m_Wrapper.m_XboxActionsCallbackInterface.OnA;
            }
            m_Wrapper.m_XboxActionsCallbackInterface = instance;
            if (instance != null)
            {
                @X.started += instance.OnX;
                @X.performed += instance.OnX;
                @X.canceled += instance.OnX;
                @A.started += instance.OnA;
                @A.performed += instance.OnA;
                @A.canceled += instance.OnA;
            }
        }
    }
    public XboxActions @Xbox => new XboxActions(this);
    private int m_XboxSchemeSchemeIndex = -1;
    public InputControlScheme XboxSchemeScheme
    {
        get
        {
            if (m_XboxSchemeSchemeIndex == -1) m_XboxSchemeSchemeIndex = asset.FindControlSchemeIndex("XboxScheme");
            return asset.controlSchemes[m_XboxSchemeSchemeIndex];
        }
    }
    public interface IXboxActions
    {
        void OnX(InputAction.CallbackContext context);
        void OnA(InputAction.CallbackContext context);
    }
}
