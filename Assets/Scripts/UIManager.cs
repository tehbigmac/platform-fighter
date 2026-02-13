using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{


public float[] playerKB = new float[2]; // PLAYER COUNT GOES HERE AND ON DECLARATIONS BELOW
public float[] playerKBPrev = new float[2];
public TextMeshProUGUI[] playerKBDisplay = new TextMeshProUGUI[2];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < playerKB.Length; i++)
        {
            playerKB[i] = 0.0f;
        }
        for (int i = 0; i < playerKBPrev.Length; i++)
        {
            playerKBPrev[i] = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < playerKB.Length; i++)
        {
            playerKBDisplay[i].text = playerKB[i].ToString("F1") + "%";
            
            playerKBPrev[i] = playerKB[i];
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerKB[0] += 6.7f;
            Debug.Log("q shi");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerKB[1] += 4.1f;
            Debug.Log("e shi");
        }

        for (int i = 0; i < playerKB.Length; i++)
        {
            if (playerKBPrev[i] != playerKB[i])
            {
                playerKBDisplay[i].text = playerKB[i].ToString("F1") + "%";
            }
        }
    }

    void KBUIEffect(TextMeshProUGUI toEffect)
    {
        
    }
}


