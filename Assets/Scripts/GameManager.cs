using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public List<PlayerController> players = new List<PlayerController>();

    private float[] KB = new float[4];

    void Start()
    {

    }


    // update game manager's variables on all the individual players (incl. kb, lives, inventory, etc)
    // these then get sent to their respective ui managers

    void Update()
    {
        for (int i = 0; i < players.Count; i++)
        {
            KB[i] = players[i].KB;
        }
    }

    public void AddPlayer(PlayerController player)
    {
        players.Add(player);
    }

}
