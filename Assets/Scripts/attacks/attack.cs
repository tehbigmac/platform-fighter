using UnityEngine;

public class attack : MonoBehaviour
{
    [Header("Data for Victim")]
    public float damage;
    public float kb;
    public float angle;

    [Header("Frame Data")]
    public float sLag;
    public float aLength;
    public float eLag;

    [Header("Data for User")]
    public string attribute;
    public float[] lagStats;
    public GameObject[] children;

    public float GetDamage() {
        return damage;
    }

    public float GetKb() {
        return kb;
    }

    public float GetAngle() {
        return angle;
    }

    public float[] GetLagPack()
    {
        lagStats = new float[] {sLag * 0.02f, aLength * 0.02f, eLag * 0.02f};

        //lagStats[0] = sLag * 0.02f;
        //lagStats[1] = aLength * 0.02f;
        //lagStats[2] = eLag * 0.02f;
        Debug.Log("lagPack exported successfully: " + lagStats[0] + " " + lagStats[1] + " " + lagStats[2]);
        return lagStats;
    }

    public string GetAttribute() {
        return attribute;
    }

    public GameObject[] GetChildren() {
        return children;
    }
}
