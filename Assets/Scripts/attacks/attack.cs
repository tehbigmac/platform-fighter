using UnityEngine;

public class attack : MonoBehaviour
{
    [Header("Data for Victim")]
    public float damage;
    public float kb;
    public float angle;
    public string kbType;

    [Header("Frame Data")]
    public float sLag;
    public float aLength;
    public float eLag;

    [Header("Data for User")]
    public string attribute;
    public bool isStrong = false;
    public GameObject[] children;
    public bool isFrame;
    
    private float[] lagStats;

    public float GetDamage() {
        return damage;
    }

    public float GetKb() {
        return kb;
    }

    public float GetAngle() {
        return angle;
    }

    public float[] GetLagPack() //takes inputted frame counts and converts them to seconds format before returning an array of floats
    {
        lagStats = new float[] {sLag * 0.02f, aLength * 0.02f, eLag * 0.02f};
        Debug.Log("lagPack exported successfully: " + lagStats[0] + " " + lagStats[1] + " " + lagStats[2]);
        return lagStats;
    }

    public string GetAttribute() {
        return attribute;
    }

    public GameObject[] GetChildren() {
        return children;
    }

    public bool IsStrong()
    {
        return isStrong;
    }

    public bool IsFrame()
    {
        return isFrame;
    }

    public float CalcHitStun (float kbValue)
    {
        Debug.Log("STUN DATA AHHHH KB: " + kbValue + " damage: " + damage + " kb: " + kb);
        if (kbValue == 0) { kbValue++; }
        var kbReturn = (Mathf.Log(kbValue * (0.5f * damage) * (0.5f * kb)) / 1000) + 2;
        if (kbReturn > 2.01f) { kbReturn = 2.01f; }
        return kbReturn;
    }
}
