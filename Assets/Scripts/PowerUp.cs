using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool doublePoints;
    public bool safeMode;

    public float powerUpLength;

    private PowerUpManager powerUpManager;

    // Start is called before the first frame update
    void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            gameObject.SetActive(false);
            powerUpManager.ActivatePowerup(doublePoints, safeMode, powerUpLength);
        }
    }
}
