using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    
    
    private PlayerController player;
    private MenuUIManager ui;
    public PlayerInput playerInput;
    private bool paused;
    private bool lastPaused;

    private InputActionReference Jump;

    void Start()
    {
        // player = GameObject.Find("meowl").GetComponent<PlayerController>();
        player = GetComponent<PlayerController>();
        ui = GameObject.Find("UI Manager").GetComponent<MenuUIManager>();

        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        paused = ui.paused;
        if (paused != lastPaused)
        {
            if (paused)
                playerInput.SwitchCurrentActionMap("UI");
            else
                playerInput.SwitchCurrentActionMap("Player");

            lastPaused = paused;
        }

        //JUMP HEIGHT DETECTION
        // if (Jump.action.ReadValueAsButton() > 0.5f) {
        //     Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
        // }
    }

// Callback method
private void OnControlsChanged(PlayerInput input)
    {
        Gamepad gamepad = input.GetDevice<Gamepad>();
        if (gamepad is UnityEngine.InputSystem.Switch.SwitchProControllerHID)
        {
            foreach (var item in Gamepad.all)
            {
                if ((item is UnityEngine.InputSystem.XInput.XInputController) && (System.Math.Abs(item.lastUpdateTime - gamepad.lastUpdateTime) < 0.1))
                {
                    Debug.Log($"Switch Pro controller detected and a copy of XInput was active at almost the same time. Disabling XInput device. `{gamepad}`; `{item}`");
                    InputSystem.DisableDevice(item);
                }
            }
        }
    }

    private void OnAttack(InputValue value) 
    {
        player.Attack(value);
    }

    private void OnStrongAttack(InputValue value)
    {
        player.StrongAttack(value);
    }

    private void OnSpecial(InputValue value)
    {
        player.Special(value);
    }

    private void OnJump(InputValue value)
    {
        player.Jump(value);
    }

    private void OnMove(InputValue value)
    {
        player.Move(value);
    }

    private void OnDodge(InputValue value) 
    {
        player.Dodge();
    }

    private void OnSubmit(InputValue value)
    {
        ui.Submit();
    }

    private void OnNavigate(InputValue value)
    {
        ui.Navigate(value);
    }
    private void OnPause(InputValue value)
    {
        ui.Pause();
    }

    private void OnResume(InputValue value)
    {
        ui.Resume();
    }
}
