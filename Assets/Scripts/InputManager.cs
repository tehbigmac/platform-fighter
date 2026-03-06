using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    
    
    private PlayerController player;
    private MenuUIManager ui;
    public PlayerInput playerInput;
    private bool paused;

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
        if (paused)
        {
            playerInput.SwitchCurrentActionMap("UI");
        }
        else
        {
            playerInput.SwitchCurrentActionMap("Player");
        }
    }

    private void OnJump(InputValue value)
    {
        player.Jump();
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
