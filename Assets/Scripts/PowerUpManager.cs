using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private bool doublePoints = false;
    private float doubleTimeLeft = 0;

    private bool safeMode = false;
    private float safeModeTimeLeft = 0;

    private ScoreManager scoreManager;
    private PlatformGenerator platformGenerator;

    public GameObject doubleParticles;
    public GameObject safeModeParticles;
    public AudioSource soundEffect;

    private float normalLaundryRate;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        platformGenerator = FindObjectOfType<PlatformGenerator>();

        normalLaundryRate = platformGenerator.laundryPercentChance;
    }

    void Update()
    {
        if (doublePoints)
        {
            doubleTimeLeft -= Time.deltaTime;
            if (doubleTimeLeft <= 0)
            {
                doublePoints = false;
                doubleParticles.SetActive(false);
                scoreManager.doubleScore = false;
            }
        }

        if (safeMode)
        {
            safeModeTimeLeft -= Time.deltaTime;
            if (safeModeTimeLeft <= 0)
            {
                safeMode = false;
                safeModeParticles.SetActive(false);
                platformGenerator.laundryPercentChance = normalLaundryRate;
            }
        }
    }

    public void ActivatePowerup(bool points, bool safe, float time)
    {
        if (points)
        {
            soundEffect.Play();
            doubleTimeLeft = time;
            scoreManager.doubleScore = true;
            if (!doublePoints)
            {
                doubleParticles.SetActive(true);
                doublePoints = true;
            }
        }

        if (safe)
        {
            soundEffect.Play();
            safeModeTimeLeft = time;
            if(!safeMode)
            {
                PlatformDestroyer[] platformList = FindObjectsOfType<PlatformDestroyer>();
                foreach (PlatformDestroyer platform in platformList)
                {
                    if (platform.name.Contains("Laundry(Clone)"))
                    {
                        platform.gameObject.SetActive(false);
                    }

                }
                platformGenerator.laundryPercentChance = 0;
                safeModeParticles.SetActive(true);
                safeMode = true;
            }
        }
    }

    public void Reset()
    {
        doublePoints = false;
        safeMode = false;
        doubleParticles.SetActive(false);
        safeModeParticles.SetActive(false);
    }
}
