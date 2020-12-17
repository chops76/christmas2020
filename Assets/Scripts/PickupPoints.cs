using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPoints : MonoBehaviour
{
    public int pointValue;

    private ScoreManager scoreManager;
    private AudioSource pickupSound;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        pickupSound = GameObject.Find("AchievementUnlocked").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if(pickupSound.isPlaying)
            {
                pickupSound.Stop();
            }
            pickupSound.Play();
            scoreManager.addScore(pointValue);
            gameObject.SetActive(false);
        }
    }
}
