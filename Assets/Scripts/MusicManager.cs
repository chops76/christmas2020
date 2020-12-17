using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource mainSong;

    // Start is called before the first frame update
    void Start()
    {
        StartMusic();
    }

    public void StartMusic()
    {
        mainSong.Play();
    }

    public void StopMusic()
    {
        mainSong.Stop();
    }
}
