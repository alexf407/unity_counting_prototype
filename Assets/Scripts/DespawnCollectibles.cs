using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnCollectibles : MonoBehaviour
{
    bool isAlive = true;
    float despawnY = 0.5f;
    float despawnDuration = 10;
    float remainingTime;
    float despawnScale = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < despawnY && isAlive)
        {
            isAlive = false;
            remainingTime = despawnDuration;
            StartCoroutine("Despawn");
        }
    }

    IEnumerator Despawn()
    {
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0)
            {
                remainingTime = 0;
                isAlive = true;
                transform.localScale = Vector3.one;
                gameObject.SetActive(false);
                break;
            }

            despawnScale = remainingTime / despawnDuration;
            transform.localScale = new Vector3(despawnScale, despawnScale, despawnScale);
            yield return null;
        }
    }
}
