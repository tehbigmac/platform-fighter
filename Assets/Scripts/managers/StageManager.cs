using UnityEngine;

public class StageManager : MonoBehaviour
{

    public GameObject[] stageStorage = new GameObject[5];

    private GameObject[] loadedStages = new GameObject[3];

    public Vector3[] stageSpawnPositions = new Vector3[2];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnStage(0);
        spawnStage(1);
        spawnStage(2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || StaticLib.stageRollReq)
        {
            StaticLib.stageRollReq = false;
            swapStage(Random.Range(0, 3));
        }
    }

    public void spawnStage(int stageToSpawn)
    {
        int stageRandomIndex = Random.Range(0, 5); 
        loadedStages[stageToSpawn] = Instantiate(stageStorage[stageRandomIndex], stageSpawnPositions[stageToSpawn], Quaternion.identity);
    }

    public void swapStage(int stageToSwap)
    {
        Destroy(loadedStages[stageToSwap]);
        spawnStage(stageToSwap);
    }
}
