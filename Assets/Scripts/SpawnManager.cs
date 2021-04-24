using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<GameObject> collectiblePrefabs;
    float spawnDelta = 0.1f;
    float elapsedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > spawnDelta)
        {
            int spawnIndex = Random.Range(0, collectiblePrefabs.Count);
            Vector3 randomPos = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(4.0f, 10.0f), 0);
            Quaternion randomRot = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
            Instantiate(collectiblePrefabs[spawnIndex], randomPos, randomRot);
            elapsedTime = 0;
        }
    }
}
