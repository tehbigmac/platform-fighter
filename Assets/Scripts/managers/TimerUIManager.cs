using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerUIManager : MonoBehaviour
{

    public float time;
    public TextMeshProUGUI timerUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }

        if (time > 0)
        {
            time -= Time.deltaTime;
            DisplayTime(time);
        }
        else
        {
            ResetTime();
        }
        
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // Optional: Adjusts display so it doesn't hit 0 early
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); // Calculate minutes
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); // Calculate seconds
        timerUI.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Format as 00:00
    }

    void ResetTime()
    {
        time = 3;
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}
