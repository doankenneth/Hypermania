using UnityEngine;
using UnityEngine.InputSystem;

public class JoinOnInput : MonoBehaviour
{
    private InputAction anyButtonAction;
    public InputDevice _P1_Input, _P2_Input = null;

    private void Awake()
    {
        anyButtonAction = new InputAction(binding: "/*/<button>");
        
        anyButtonAction.performed += OnAnyButtonPressed;
        anyButtonAction.Enable();
    }
    void OnEnable()
    {
        anyButtonAction.Enable();
    }

    void OnDisable()
    {
        anyButtonAction.Disable();
    }
    private void OnAnyButtonPressed(InputAction.CallbackContext context)
    {
        if (context.control.device is not Mouse) {
            if (_P1_Input == null)
            {
                _P1_Input = context.control.device;
                Debug.Log("Player 1: " + _P1_Input.name);
            }
            else if (_P2_Input == null && context.control.device != _P1_Input)
            {
                _P2_Input = context.control.device;
                Debug.Log("Player 2: " + _P2_Input.name);
            }
        }
        if (_P1_Input == null || _P2_Input == null)
        {
            anyButtonAction = new InputAction(binding: "/*/<button>");
        }
        else {
            anyButtonAction.Disable();
        }
        


    }

    public InputDevice GetPlayerInputDevice(int i) {
        if (i == 1) {
            return _P1_Input;
        }
        if (i == 2)
        {
            return _P2_Input;
        }
        return null;
    }
}
