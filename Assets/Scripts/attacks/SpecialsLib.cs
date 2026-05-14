using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UIElements.Experimental;
using System;

public class SpecialsLib : MonoBehaviour
{
    private PlayerController playerController;

    public GameObject blast;
    public GameObject hiJump;
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
        blast.SetActive(true);
        blast.transform.Find("frame1").gameObject.SetActive(true);
        StartCoroutine(SwitchObjects(0.3f, blast.transform.Find("frame1").gameObject, 0.1f, blast.transform.Find("frame2").gameObject, 0.5f));
    }

    public void HiJump() 
    {
        // playerController.jumping = true;
        StartCoroutine(playerController.SwapInAttack(0.5f));
    }

    public IEnumerator SwitchObjects(float s, GameObject gb, float s2, GameObject gb2, float s3)
    {
        yield return new WaitForSeconds(s);
        gb.SetActive(false);
        yield return new WaitForSeconds(s2);
        gb2.SetActive(true);
        yield return new WaitForSeconds(s3);
        gb2.SetActive(false);
    }
}