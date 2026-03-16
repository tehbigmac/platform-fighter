using UnityEngine;

public class SpecialsList : MonoBehaviour
{
    public static string[] specialsList = new string[4];

    public void Start()
    {
        specialsList[0] = "blast";
        specialsList[1] = "hijump";
        specialsList[2] = "slam";
        specialsList[3] = "dash";
        specialsList[4] = "laser";
    }
}
