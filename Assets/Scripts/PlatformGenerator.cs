using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    private int platformSelector = 0;
    public Transform generationPoint;
    public ObjectPool[] objectPools;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    public Transform maxHeightPoint;
    private float minHeight;
    private float maxHeight;
    public float maxHeightChange;

    private PickupGenerator pickupGenerator;
    public float pickupPercentChance;

    public float laundryPercentChance;
    public ObjectPool laundryPool;
   
    private float[] platformWidths;

    // Start is called before the first frame update
    void Start()
    {
        platformWidths = new float[objectPools.Length];
        for(int i = 0; i < objectPools.Length; ++i)
        {
            platformWidths[i] = objectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        pickupGenerator = FindObjectOfType<PickupGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < generationPoint.position.x)
        {
            float distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
            platformSelector = Random.Range(0, objectPools.Length);

            float newHeight = transform.position.y + Random.Range(-maxHeightChange, maxHeightChange);
            newHeight = System.Math.Min(maxHeight, System.Math.Max(newHeight, minHeight));

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector]/2) + distanceBetween,
                newHeight, transform.position.z);

            GameObject new_obj = objectPools[platformSelector].GetPooledObject();
            new_obj.transform.position = transform.position;
            new_obj.transform.rotation = transform.rotation;
            new_obj.SetActive(true);

            if (Random.Range(0, 99) < pickupPercentChance)
            {
                pickupGenerator.SpawnPickups(new Vector3(transform.position.x, 
                    transform.position.y + 1.5f, transform.position.z));
            }

            if (Random.Range(0, 99) < laundryPercentChance)
            {
                GameObject newLaundry = laundryPool.GetPooledObject();
                newLaundry.transform.position = transform.position + 
                    new Vector3(Random.Range(-platformWidths[platformSelector] / 2, 
                                             platformWidths[platformSelector] / 2), .5f, 0);
                newLaundry.transform.rotation = transform.rotation;
                newLaundry.SetActive(true);
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2),
                transform.position.y, transform.position.z);
        }
    }
}
