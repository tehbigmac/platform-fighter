using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UIElements.Experimental;
using System;

public class SpecialsLib : MonoBehaviour
{
    private PlayerController playerController;

    public GameObject blast;
    public GameObject slam;
    public GameObject dash;
    public GameObject laser;

    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        SpecialsList.upSpecialsList[0] = "hijump";
        SpecialsList.upSpecialsList[1] = "warp";

        SpecialsList.downSpecialsList[0] = "blast";
        SpecialsList.downSpecialsList[1] = "slam";

        SpecialsList.sideSpecialsList[0] = "dash";
        SpecialsList.sideSpecialsList[1] = "laser";
    }

    public void IndexUpSpecials(string requestedSp, InputValue value) 
    {
        if (requestedSp == SpecialsList.upSpecialsList[0])
        {
            Debug.Log("hijump Matched! augh");
            HiJump();
        }
    }
    public void HiJump() //hijump is REALLY CHOPPED we need a better system someday lmao
        {
            playerController.inAttack = false;
            playerController.inMovingAttack = true;
            playerController.toFall = 0;
            playerController.toJump = 0;
            playerController.jumps = 0;
            playerController.ReceiveAttack(0, 41, 90);
            StartCoroutine(playerController.YouCanAttackNow(0.5f));
        }

    //downspecials
    public void IndexDownSpecials(string requestedSp, InputValue value)
    {
        if (requestedSp == SpecialsList.downSpecialsList[0])
        {
            Debug.Log("blast Matched! augh");
            Blast();
        }
    }
    public void Blast() 
    {
        Debug.Log("poop debug");
        playerController.SortAttackType(blast);
    }

    //sidespecials
    public void IndexSideSpecials(string requestedSp, InputValue value)
    {
        if (requestedSp == SpecialsList.downSpecialsList[0])
        {
            Debug.Log("side blast Matched! augh");
            Blast();
        }
    }

    //other stuff
    public void IndexNeutralSpecials(string requestedSp, InputValue value)
    {
        if (requestedSp == SpecialsList.downSpecialsList[0])
        {
            Debug.Log(" blast Matched! augh");
            Blast();
        }
    }
    public void DisableAllSpecials() //unfortunately will be hard coded :( specials are too wacky to not be
    {
        blast.transform.Find("frame1").gameObject.SetActive(false);
        blast.transform.Find("frame2").gameObject.SetActive(false);
    }
}