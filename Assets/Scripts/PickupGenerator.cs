using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGenerator : MonoBehaviour
{
    public ObjectPool[] objectPools;
    public float distanceBetweenPickups;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnPickups(Vector3 startPosition)
    {
        int pickupType = Random.Range(0, objectPools.Length);
        GameObject pickup1 = objectPools[pickupType].GetPooledObject();
        pickup1.transform.position = startPosition;
        pickup1.SetActive(true);

        GameObject pickup2 = objectPools[pickupType].GetPooledObject();
        pickup2.transform.position = 
            new Vector3(startPosition.x - distanceBetweenPickups, startPosition.y, startPosition.z);
        pickup2.SetActive(true);

        GameObject pickup3 = objectPools[pickupType].GetPooledObject();
        pickup3.transform.position = new Vector3(startPosition.x + distanceBetweenPickups, startPosition.y, startPosition.z); ;
        pickup3.SetActive(true);
    }
}
