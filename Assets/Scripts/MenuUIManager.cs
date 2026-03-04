using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class MenuUIManager : MonoBehaviour
{

    public GameObject MenuUI;
    public TextMeshProUGUI[] menuElements = new TextMeshProUGUI[3];
    public int selectedIndex;

    public bool paused;
    public bool navEligible;
    public float stickThreshold;

    void Start()
    {
        Resume();
        selectedIndex = menuElements.Length;
        paused = false;
        stickThreshold = 0.5f; // point at which stick will navigate
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("p shi");
            if (!paused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }

        // if (Input.GetKeyDown(KeyCode.Return) && paused)
        // {
        //     Debug.Log("enter shi");
        //     if (selectedIndex == 0)
        //     {
        //         Resume();
        //     }
        //     if (selectedIndex == 1)
        //     {
        //         Options();
        //     }
        //     if (selectedIndex == 2)
        //     {
        //         Exit();
        //     }
        // }

    }

    public void Submit()
    {
        if (paused)
        {
            Debug.Log("enter shi");
            if (selectedIndex == 0)
            {
                Resume();
            }
            if (selectedIndex == 1)
            {
                Options();
            }
            if (selectedIndex == 2)
            {
                Exit();
            }
        }
    }

    public void Navigate(InputValue value)
    {
        if (value.Get<Vector2>().y > -stickThreshold && value.Get<Vector2>().y < stickThreshold)
        {
            navEligible = true;
        }
        if (value.Get<Vector2>().y <= -stickThreshold && navEligible)
        {
            Debug.Log("down shi");
            navEligible = false;
            if (selectedIndex < menuElements.Length - 1)
            {
                selectedIndex ++;
                UpdateSelection();
            }
        }
        if (value.Get<Vector2>().y >= stickThreshold && navEligible)
        {
            Debug.Log("up shi");
            navEligible = false;
            if (selectedIndex > 0)
            {
                selectedIndex --;
                UpdateSelection();
            }
        }
    }

    public void Pause()
    {
        paused = true;
        // Time.timeScale = 0f;
        MenuUI.SetActive(true);
        selectedIndex = 0;
        UpdateSelection();
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1f;
        MenuUI.SetActive(false);
    }

    public void Options()
    {
        Debug.Log("options");
    }

    public void Exit()
    {
        Debug.Log("exit");
    }
    public void UpdateSelection()
    {
        for (int i = 0; i < menuElements.Length; i++)
        {
            menuElements[i].color = new Color(1f, 1f, 1f, 1f);
        }
        menuElements[selectedIndex].color = new Color(1f, 1f, 0f, 1f);
    }
}