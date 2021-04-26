using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] List<GameObject> collectiblePrefabs;
    [SerializeField] int poolingSize = 200;
    [SerializeField] float spawnTime = 1.0f;
    [SerializeField] float spawnRangeX = 7.0f;
    [SerializeField] float spawnRangeZ = 0.7f;
    [SerializeField] float spawnRangeMinY = 4.0f;
    [SerializeField] float spawnRangeMaxY = 10.0f;
    [SerializeField] float halfPlayerSizeX = 2.0f;
    [SerializeField] float playerAvoidanceX = 5.0f;
    [SerializeField] float randomTorqueRange = 50.0f;
    GameObject[] collectiblesPool;
    bool isSpawnerActive = false;

    void Start()
    {
        PopulatePool();
    }

    public void StartSpawner()
    {
        isSpawnerActive = true;
        StartCoroutine("Spawner");
    }

    public void StopSpawner()
    {
        isSpawnerActive = false;
    }

    public void FreezeItems()
    {
        foreach (GameObject collectible in collectiblesPool)
        {
            collectible.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    IEnumerator Spawner()
    {
        while (isSpawnerActive)
        {
            yield return new WaitForSeconds(spawnTime);

            SpawnCollectible();
        }
    }

    void SpawnCollectible()
    {
        foreach (GameObject collectible in collectiblesPool)
        {
            if (!collectible.activeSelf)
            {
                // Set collectible to random position and rotation
                float spawnX = Random.Range(-spawnRangeX, spawnRangeX);
                // Do not spawn items over player
                if (spawnX > playerObject.transform.position.x - halfPlayerSizeX && spawnX < playerObject.transform.position.x + halfPlayerSizeX) {
                    spawnX += spawnX > 0 ? -playerAvoidanceX : playerAvoidanceX;
                }
                Vector3 randomPos = new Vector3(spawnX, Random.Range(spawnRangeMinY, spawnRangeMaxY), Random.Range(-spawnRangeZ, spawnRangeZ));
                Quaternion randomRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

                collectible.transform.position = randomPos;
                collectible.transform.rotation = randomRotation;

                Rigidbody rb = collectible.GetComponent<Rigidbody>();

                // Reset velocity to prevent acceleration after reuse
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                // Add random rotation
                Vector3 randomTorque = new Vector3(Random.Range(-randomTorqueRange, randomTorqueRange), Random.Range(-randomTorqueRange, randomTorqueRange), Random.Range(-randomTorqueRange, randomTorqueRange));
                rb.AddRelativeTorque(randomTorque, ForceMode.Impulse);

                collectible.SetActive(true);

                break;
            }
        }
            
    }

    void PopulatePool()
    {
        collectiblesPool = new GameObject[poolingSize];

        for (int i = 0; i < poolingSize; i++)
        {
            int spawnIndex = Random.Range(0, collectiblePrefabs.Count);
            collectiblesPool[i] = Instantiate(collectiblePrefabs[spawnIndex]);
            collectiblesPool[i].SetActive(false);
        }
    }
}
