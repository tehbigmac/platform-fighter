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
        SpecialsList.specialsList[0] = "blast";
        SpecialsList.specialsList[1] = "hijump";
        SpecialsList.specialsList[2] = "slam";
        SpecialsList.specialsList[3] = "dash";
        SpecialsList.specialsList[4] = "laser";
    }

    public void IndexSpecials(string requestedSp, InputValue value) 
    {
        Debug.Log("augh comparing " + requestedSp + " with " + SpecialsList.specialsList[1]);
        if (requestedSp == SpecialsList.specialsList[0])
        {
            Debug.Log(" blast Matched! augh");
            Blast();
        }

        if (requestedSp == SpecialsList.specialsList[1])
        {
            Debug.Log("Matched! augh");
            HiJump();
        }
    }

    public void Blast() 
    {
        Debug.Log("poop debug");
        playerController.SortAttackType(blast);
        //blast.SetActive(true);
        //blast.transform.Find("frame1").gameObject.SetActive(true);
        //StartCoroutine(SwitchObjects(0.3f, blast.transform.Find("frame1").gameObject, 0.1f, blast.transform.Find("frame2").gameObject, 0.5f));
    }

    public void HiJump() //hijump is REALLY CHOPPED we need a better system someday lmao
    {
        playerController.inAttack = false;
        playerController.inMovingAttack = true;
        playerController.toFall = 0;
        playerController.toJump = 0;
        playerController.jumps = 0;
        playerController.ReceiveAttack(0, 41, 90, Vector3.zero, transform.gameObject);
        StartCoroutine(playerController.YouCanAttackNow(0.5f));
    }

    public void DisableAllSpecials() //unfortunately will be hard coded :( specials are too wacky to not be
    {
        blast.transform.Find("frame1").gameObject.SetActive(false);
        blast.transform.Find("frame2").gameObject.SetActive(false);
    }
}