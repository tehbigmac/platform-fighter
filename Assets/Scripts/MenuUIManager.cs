using UnityEngine;
using TMPro;

public class MenuUIManager : MonoBehaviour
{

    public GameObject MenuUI;
    public TextMeshProUGUI[] menuElements = new TextMeshProUGUI[3];
    public int selectedIndex;

    public bool paused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Resume();
        selectedIndex = menuElements.Length;
    }

    // Update is called once per frame
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
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("down shi");
            if (selectedIndex < menuElements.Length - 1)
            {
                selectedIndex ++;
                UpdateSelection();
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("up shi");
            if (selectedIndex > 0)
            {
                selectedIndex --;
                UpdateSelection();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && paused)
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

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0f;
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