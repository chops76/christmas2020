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

    public DeathMenu deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        platformStartPoint = platformGenerator.position;
        playerStartPoint = player.transform.position;
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        scoreManager.scoreIncreasing = false;
        scoreManager.SaveHighScore();
        player.gameObject.SetActive(false);
        deathScreen.gameObject.SetActive(true);
    }

    public void Reset()
    {
        deathScreen.gameObject.SetActive(false);
        PlatformDestroyer[] platformList = FindObjectsOfType<PlatformDestroyer>();
        foreach (PlatformDestroyer platform in platformList)
        {
            platform.gameObject.SetActive(false);
        }
        player.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        scoreManager.scoreCount = 0;
        scoreManager.scoreIncreasing = true;
        player.gameObject.SetActive(true);
    }
}
