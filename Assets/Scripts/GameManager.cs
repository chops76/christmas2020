using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform platformGenerator;
    private Vector3 platformStartPoint;

    public PlayerControl player;
    private Vector3 playerStartPoint;

    private ScoreManager scoreManager;
    private MusicManager musicManager;
    private PowerUpManager powerUpManager;

    public DeathMenu deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        platformStartPoint = platformGenerator.position;
        playerStartPoint = player.transform.position;
        scoreManager = FindObjectOfType<ScoreManager>();
        musicManager = FindObjectOfType<MusicManager>();
        powerUpManager = FindObjectOfType<PowerUpManager>();
    }

    public void RestartGame()
    {
        scoreManager.scoreIncreasing = false;
        scoreManager.SaveHighScore();
        deathScreen.gameObject.SetActive(true);
        musicManager.StopMusic();
    }

    public void Reset()
    {
        deathScreen.gameObject.SetActive(false);
        powerUpManager.Reset();
        PlatformDestroyer[] platformList = FindObjectsOfType<PlatformDestroyer>();
        foreach (PlatformDestroyer platform in platformList)
        {
            platform.gameObject.SetActive(false);
        }
        player.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        scoreManager.scoreCount = 0;
        scoreManager.scoreIncreasing = true;
        player.Reset();
        player.gameObject.SetActive(true);
        musicManager.StartMusic();
    }
}
