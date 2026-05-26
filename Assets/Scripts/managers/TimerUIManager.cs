using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerUIManager : MonoBehaviour
{

    public float time;
    public TextMeshProUGUI timerUI;

    private float timeLimit = 180;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTime();
        }
        
        Debug.Log("time is " + time);

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
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void ResetTime()
    {
        time = timeLimit;
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}
