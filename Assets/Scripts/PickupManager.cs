using UnityEngine;

public class PickupManager : MonoBehaviour
{
    
    public Vector3[] pickupSpawnPositions = new Vector3[3];

    public GameObject rerollStagePrefab;
    public GameObject rerollSpecialPrefab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int rando = Random.Range(0, 100);
        if (rando == 0)
        {
            SpawnPickup(rerollStagePrefab);
        }

        if (rando == 1)
        {
            SpawnPickup(rerollSpecialPrefab);
        }
    }

    void SpawnPickup(GameObject prefab)
    {
        Instantiate(prefab, pickupSpawnPositions[Random.Range(0, 3)], Quaternion.Euler(0f, 140f, 0f));
    }
}
